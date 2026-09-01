import {
    Component,
    Input,
    OnChanges,
    SimpleChanges,
    OnDestroy,
    ElementRef,
    ViewChild,
} from '@angular/core';
import { Chart, ChartConfiguration, registerables } from 'chart.js';
import { Budget } from '../entities/Budget';
  
Chart.register(...registerables);

@Component({
    selector: 'app-budget-chart',
    templateUrl: './budget-chart.component.html',
    styleUrls: ['./budget-chart.component.css']
})
export class BudgetChartComponent implements OnChanges, OnDestroy {
    @Input() budget!: Budget;
  
    @ViewChild('expenseChartCanvas', { static: true }) expenseChartCanvas!: ElementRef;
    @ViewChild('incomeChartCanvas', { static: true }) incomeChartCanvas!: ElementRef;
  
    expenseChart!: Chart;
    incomeChart!: Chart;
  
    ngOnChanges(changes: SimpleChanges): void {
      if (changes['budget'] && this.budget) {
        this.renderExpenseChart();
        this.renderIncomeChart();
      }
    }
  
    renderExpenseChart(): void {
      const expenses = this.budget.expenses || [];
      const expenseLabels = expenses.map(i => i.name) ?? [];
  
      const data = {
        labels: [...expenseLabels],
        datasets: [
          {
            label: 'Total Expense',
            data: expenses.map(e => e.amount ?? 0),
          }
        ],
      };
  
      const config: ChartConfiguration = {
        type: 'pie',
        data,
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              position: 'top',
            },
          },
        },
      };
  
      if (this.expenseChart) {
        this.expenseChart.destroy();
      }
  
      const ctx = this.expenseChartCanvas.nativeElement.getContext('2d');
      this.expenseChart = new Chart(ctx, config);
    }
  
    renderIncomeChart(): void {
      const income = this.budget.incomeSources || [];
      const incomeLabels = income.map(i => i.name) ?? [];
  
      const data = {
        labels: [...incomeLabels],
        datasets: [
          {
            label: 'Total Income',
            data: income.map(i => i.amount ?? 0),
          }
        ],
      };
  
      const config: ChartConfiguration = {
        type: 'pie',
        data,
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: 'top',
            },
          },
        },
      };
  
      if (this.incomeChart) {
        this.incomeChart.destroy();
      }
  
      const ctx = this.incomeChartCanvas.nativeElement.getContext('2d');
      this.incomeChart = new Chart(ctx, config);
    }
  
    ngOnDestroy(): void {
        if (this.expenseChart) {
            this.expenseChart.destroy();
        }
        if (this.incomeChart) {
            this.incomeChart.destroy();
        }
    }

    refreshChart(): void {
        if (this.budget) {
          this.renderExpenseChart();
          this.renderIncomeChart();
        }
      }
      
}
  