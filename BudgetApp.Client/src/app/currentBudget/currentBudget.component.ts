import { Component, EventEmitter, Input, Output, SimpleChanges, ViewChild } from "@angular/core";
import { Budget } from "../entities/Budget";
import { CdkDragDrop, moveItemInArray } from "@angular/cdk/drag-drop";
import { BudgetService } from "../services/budget.service";
import { BudgetChartComponent } from "../chart/budget-chart.component";
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";

@Component({
  selector: 'app-currentBudget',
  templateUrl: './currentBudget.component.html',
})
export class CurrentBudgetComponent {
  @Input() budget!: Budget;
  @Output() notifyParentEditExpense: EventEmitter<{ expenseId: string, expenseName: string, expenseAmount: number }> = new EventEmitter();
  @Output() notifyParentRemoveExpense: EventEmitter<{ expenseId: string, expenseName: string }> = new EventEmitter();
  @Output() notifyParentEditIncomeSource: EventEmitter<{ incomeSourceId: string, incomeSourceName: string, incomeSourceAmount: number }> = new EventEmitter();
  @Output() notifyParentRemoveIncomeSource: EventEmitter<{ incomeSourceId: string, incomeSourceName: string }> = new EventEmitter();
  @ViewChild(BudgetChartComponent) budgetChartComp!: BudgetChartComponent;

  totalExpenses: number = 0;
  totalIncome: number = 0;
  activeTab: string | undefined;

  constructor(private budgetService: BudgetService) {}

  onTabChange(tab: string) {
    this.activeTab = tab;
    if (tab === 'chart') {
      setTimeout(() => {
        this.budgetChartComp?.refreshChart();
      }, 0); // Ensure DOM is updated
    }
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if(changes['budget'] && this.budget) {
      this.totalExpenses = this.budget.expenses?.reduce((sum, expense) => sum + (expense.amount || 0), 0);
      this.totalIncome = this.budget.incomeSources?.reduce((sum, incomeSource) => sum + (incomeSource.amount || 0), 0);
    }
  }

  editExpense(expenseId: string, expenseName: string, expenseAmount: number) {
    let payload = {
      expenseId: expenseId,
      expenseName: expenseName,
      expenseAmount: expenseAmount,
    };

    this.notifyParentEditExpense.emit(payload);
  }

  removeExpense(expenseId: string, expenseName: string) {
    let payload = {
      expenseId: expenseId,
      expenseName: expenseName,
    };

    this.notifyParentRemoveExpense.emit(payload);
  }

  editIncomeSource(incomeSourceId: string, incomeSourceName: string, incomeSourceAmount: number) {
    let payload = {
      incomeSourceId: incomeSourceId,
      incomeSourceName: incomeSourceName,
      incomeSourceAmount: incomeSourceAmount,
    };

    this.notifyParentEditIncomeSource.emit(payload);
  }

  removeIncomeSource(incomeSourceId: string, incomeSourceName: string) {
    let payload = {
      incomeSourceId: incomeSourceId,
      incomeSourceName: incomeSourceName,
    };

    this.notifyParentRemoveIncomeSource.emit(payload);
  }

  dropExpense(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.budget.expenses, event.previousIndex, event.currentIndex);
    this.budget.expenses.forEach((expense, index) => {
      expense.sortOrder = index;
    });

    this.budgetService.saveOrientation(this.budget).subscribe();
  }

  dropIncomeSource(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.budget.incomeSources, event.previousIndex, event.currentIndex);
    this.budget.incomeSources.forEach((incomeSource, index) => {
      incomeSource.sortOrder = index;
    });
    this.budgetService.saveOrientation(this.budget).subscribe();
  }

  generateBudgetPdf(): void {
    const doc = new jsPDF();
  
    doc.setFontSize(18);
    doc.text('Budget Summary', 14, 20);
  
    const body: any[] = [];
  
    // Add Expenses Section
    body.push([{ content: 'Expenses', colSpan: 2, styles: { halign: 'left', fillColor: [220, 235, 255], textColor: [0, 0, 0] } }]);
    this.budget.expenses.forEach(expense => {
      body.push([expense.name, `$${expense.amount?.toFixed(2) || '0.00'}`]);
    });
  
    // Add Income Section
    body.push([{ content: 'Income Sources', colSpan: 2, styles: { halign: 'left', fillColor: [220, 235, 255], textColor: [0, 0, 0] } }]);
    this.budget.incomeSources.forEach(income => {
      body.push([income.name, `$${income.amount?.toFixed(2) || '0.00'}`]);
    });
  
    let difference = this.totalIncome - this.totalExpenses;
    // Add difference
    body.push([
      { 
        content: 'Difference',
        styles: { halign: 'left', fillColor: [220, 235, 255], textColor: [0, 0, 0] }
      },
      { 
        content: difference < 0 ? `-$${Math.abs(difference).toFixed(2)}` : `$${difference}`,
        styles: { halign: 'left', fillColor: [220, 235, 255], textColor: difference < 0 ? [220, 53, 69] : [25, 135, 84] }
      },
    ]);
  
    // Generate the table
    autoTable(doc, {
      body,
      startY: 30,
      theme: 'grid',
      styles: { fontSize: 11 },
      headStyles: { fillColor: [33, 37, 41] }, // Bootstrap dark gray
    });

    // Finally, add the charts to the PDF too
    // doc.addPage();
    // let expenseChart = (document.getElementById('expenseChartCanvas') as HTMLCanvasElement).toDataURL('image/jpg');
    
    // doc.addImage(expenseChart, 'JPG', 10, 10, 180, 180);

    // doc.addPage();
    // let incomeChart = (document.getElementById('incomeChartCanvas') as HTMLCanvasElement).toDataURL('image/jpg');
    // doc.addImage(incomeChart, 'JPG', 10, 10, 180, 180);

    doc.save('budget-summary.pdf');
  }
  
  
}
