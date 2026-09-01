import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Budget } from '../entities/Budget';
import { BudgetService as BudgetService } from '../services/budget.service';
import { Component } from '@angular/core';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-budgets',
  templateUrl: './budgets.component.html',
})
export class BudgetsComponent {
  budget!: Budget;
  error: string = '';

  expenseId: string = '';
  removeExpenseId: string = '';
  expenseName: string = '';
  removeExpenseName: string = '';
  expenseAmount: number = 0;

  incomeSourceId: string = '';
  removeIncomeSourceId: string = '';
  incomeSourceName: string = '';
  removeIncomeSourceName: string = '';
  incomeSourceAmount: number = 0;

  budgetDifference: number = 0;

  constructor(private router: Router, private toastr: ToastrService, private budgetService: BudgetService) {
    // Check if the state exists and show the toast message
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state?.['showToast']) {
      const message = navigation.extras.state['toastMessage'] || 'Action was successful';
      const title = navigation.extras.state['toastTitle'] || 'Info';
      this.toastr.success(message, title);
    }

    this.budgetService.getCurrentBudget().subscribe({
      next: (result) => {
        this.budget = result;
        this.calculateAmounts();
      }
    });
  }

  ngOnInit() {
    (document.getElementById('expenseModal') as HTMLElement).addEventListener('shown.bs.modal', () => {
      (document.getElementById("expenseName") as HTMLInputElement).focus();
    });
    (document.getElementById('incomeSourceModal') as HTMLElement).addEventListener('shown.bs.modal', () => {
      (document.getElementById("incomeSourceName") as HTMLInputElement).focus();
    });
  }

  calculateAmounts() {
    let totalExpenses = this.budget?.expenses?.reduce((sum, expense) => sum + (expense.amount || 0), 0);
    let totalIncome = this.budget?.incomeSources?.reduce((sum, incomeSource) => sum + (incomeSource.amount || 0), 0);

    this.budgetDifference = (totalIncome ?? 0) - (totalExpenses ?? 0);
  }

  // ------------ Expenses ------------------ //
  editExpense(expenseData: { expenseId: string, expenseName: string, expenseAmount: number }) {
    (document.getElementById("expenseId") as HTMLInputElement).value = expenseData.expenseId;
    (document.getElementById("expenseName") as HTMLInputElement).value = expenseData.expenseName;
    (document.getElementById("expenseAmount") as HTMLInputElement).value = expenseData.expenseAmount.toString();
    this.expenseId = expenseData.expenseId;
    this.expenseName = expenseData.expenseName;
    this.expenseAmount = expenseData.expenseAmount as number;

    let modalEl = document.getElementById("expenseModal") as HTMLElement;

    let currentTitle = modalEl.querySelector('.modal-title')!.textContent;
    let currentButtonText = modalEl.querySelector('.actionButton')!.textContent;

    modalEl.querySelector('.modal-title')!.textContent = 'Edit Expense';
    modalEl.querySelector('.actionButton')!.textContent = 'Save';
    modalEl.querySelector('.actionButton')!.setAttribute('data-updating', 'true');

    let expenseModal = Modal.getOrCreateInstance(modalEl as Element);
    expenseModal.show();

    modalEl.addEventListener('hidden.bs.modal', () => {
      //put the button state back
      modalEl.querySelector('.modal-title')!.textContent = currentTitle;
      modalEl.querySelector('.actionButton')!.textContent = currentButtonText;
      modalEl.querySelector('.actionButton')!.setAttribute('data-updating', 'false');

      // reset the values
      (document.getElementById("expenseId") as HTMLInputElement).value = "";
      this.expenseId = "";
      (document.getElementById("expenseName") as HTMLInputElement).value = "";
      (document.getElementById("expenseAmount") as HTMLInputElement).value = "";
    }, { once: true });
  }

  addOrUpdateExpense(expense: HTMLButtonElement) {
    let updating = expense.getAttribute('data-updating') === 'true';

    if (updating) {
      this.budgetService.updateExpense(this.expenseId, this.expenseName, this.expenseAmount).subscribe({
        next: (result: { budget: Budget; }) => {
          this.toastr.success('Your "' + this.expenseName + '" expense has been updated!', 'Success!');
          this.budget = result.budget;
          this.calculateAmounts();
        },
        error: () => { 
          this.toastr.error('Unable to add new expense.', 'Error!');
        }
      });
    } else {
      this.budgetService.addExpense(this.expenseName, this.expenseAmount).subscribe({
        next: (result: { budget: Budget; }) => {
          this.toastr.success('Your "' + this.expenseName + '" expense has been added!', 'Success!');
          this.budget = result.budget;
          this.calculateAmounts();
        },
        error: () => {
          this.toastr.error('Unable to add new expense.', 'Error!');
        }
      });
    }

    let modalEl = document.getElementById("expenseModal") as HTMLElement;
    let expenseModal = Modal.getOrCreateInstance(modalEl as Element);
    expenseModal.hide();
  }

  removeExpenseModal(expenseData: { expenseId: string, expenseName: string }) {
    this.removeExpenseId = expenseData.expenseId;
    this.removeExpenseName = expenseData.expenseName;

    let modalEl = document.getElementById("removeExpenseConfirmationModal") as HTMLElement;

    let removeExpenseModal = Modal.getOrCreateInstance(modalEl as Element);
    removeExpenseModal.show();
  }

  removeExpense() {
    this.budgetService.removeExpense(this.removeExpenseId).subscribe({
      next: (result: { budget: Budget; }) => {
        this.toastr.success('Your "' + this.removeExpenseName + '" expense has been removed.', 'Success!');
        this.budget = result.budget;
        this.calculateAmounts();
      },
      error: () => { 
        this.toastr.error('Unable to remove expense.', 'Error!');
      }
    });

    let modalEl = document.getElementById("removeExpenseConfirmationModal") as HTMLElement;
    let removeExpenseModal = Modal.getOrCreateInstance(modalEl as Element);
    removeExpenseModal.hide();
  }

  // -------------- Income Sources ------------------ //
  editIncomeSource(incomeSourceData: { incomeSourceId: string, incomeSourceName: string, incomeSourceAmount: number }) {
    (document.getElementById("incomeSourceId") as HTMLInputElement).value = incomeSourceData.incomeSourceId;
    (document.getElementById("incomeSourceName") as HTMLInputElement).value = incomeSourceData.incomeSourceName;
    (document.getElementById("incomeSourceAmount") as HTMLInputElement).value = incomeSourceData.incomeSourceAmount.toString();
    this.incomeSourceId = incomeSourceData.incomeSourceId;
    this.incomeSourceName = incomeSourceData.incomeSourceName;
    this.incomeSourceAmount = incomeSourceData.incomeSourceAmount as number;

    let modalEl = document.getElementById("incomeSourceModal") as HTMLElement;

    let currentTitle = modalEl.querySelector('.modal-title')!.textContent;
    let currentButtonText = modalEl.querySelector('.actionButton')!.textContent;

    modalEl.querySelector('.modal-title')!.textContent = 'Edit Income Source';
    modalEl.querySelector('.actionButton')!.textContent = 'Save';
    modalEl.querySelector('.actionButton')!.setAttribute('data-updating', 'true');

    let incomeSourceModal = Modal.getOrCreateInstance(modalEl as Element);
    incomeSourceModal.show();

    modalEl.addEventListener('hidden.bs.modal', () => {
      //put the button state back
      modalEl.querySelector('.modal-title')!.textContent = currentTitle;
      modalEl.querySelector('.actionButton')!.textContent = currentButtonText;
      modalEl.querySelector('.actionButton')!.setAttribute('data-updating', 'false');

      // reset the values
      (document.getElementById("incomeSourceId") as HTMLInputElement).value = "";
      this.incomeSourceId = "";
      (document.getElementById("incomeSourceName") as HTMLInputElement).value = "";
      (document.getElementById("incomeSourceAmount") as HTMLInputElement).value = "";
    }, { once: true });
  }

  addOrUpdateIncomeSource(incomeSource: HTMLButtonElement) {
    let updating = incomeSource.getAttribute('data-updating') === 'true';

    if (updating) {
      this.budgetService.updateIncomeSource(this.incomeSourceId, this.incomeSourceName, this.incomeSourceAmount).subscribe({
        next: (result: { budget: Budget; }) => {
          this.toastr.success('Your "' + this.incomeSourceName + '" income source has been updated!', 'Success!');
          this.budget = result.budget;
          this.calculateAmounts();
        },
        error: () => { 
          this.toastr.error('Unable to update new income source.', 'Error!');
        }
      });
    } else {
      this.budgetService.addIncomeSource(this.incomeSourceName, this.incomeSourceAmount).subscribe({
        next: (result: { budget: Budget; }) => {
          this.toastr.success('Your "' + this.incomeSourceName + '" income source has been added!', 'Success!');
          this.budget = result.budget;
          this.calculateAmounts();
        },
        error: () => { 
          this.toastr.error('Unable to add new income source.', 'Error!');
        }
      });
    }
    
    let modalEl = document.getElementById("incomeSourceModal") as HTMLElement;
    let incomeSourceModal = Modal.getOrCreateInstance(modalEl as Element);
    incomeSourceModal.hide();
  }

  removeIncomeSourceModal(incomeSourceData: { incomeSourceId: string, incomeSourceName: string }) {
    this.removeIncomeSourceId = incomeSourceData.incomeSourceId;
    this.removeIncomeSourceName = incomeSourceData.incomeSourceName;

    let modalEl = document.getElementById("removeIncomeSourceConfirmationModal") as HTMLElement;

    let removeIncomeSourceModal = Modal.getOrCreateInstance(modalEl as Element);
    removeIncomeSourceModal.show();
  }

  removeIncomeSource() {
    this.budgetService.removeIncomeSource(this.removeIncomeSourceId).subscribe({
      next: (result: { budget: Budget; }) => {
        this.toastr.success('Your "' + this.removeIncomeSourceName + '" income source has been removed.', 'Success!');
        this.budget = result.budget;
        this.calculateAmounts();
      },
      error: () => { 
        this.toastr.error('Unable to remove your income source.', 'Error!');
      }
    });
    
    let modalEl = document.getElementById("removeIncomeSourceConfirmationModal") as HTMLElement;
    let removeIncomeSourceModal = Modal.getOrCreateInstance(modalEl as Element);
    removeIncomeSourceModal.hide();
  }
}
