import { Component, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Budget } from '../entities/Budget';
import { User } from '../entities/User';
import { BudgetsService } from '../services/budgets.service';

@Component({
  selector: 'app-budgets',
  templateUrl: './budgets.component.html',
})
export class BudgetsComponent {
  budget: Budget;
  user: User;
  error: string = '';
  expenseName: string = '';
  expenseAmount: number = 0;
  incomeSourceName: string = '';
  incomeSourceAmount: number = 0;

  constructor(private router: Router, private toastr: ToastrService, private budgetsService: BudgetsService,) {
    // Check if the state exists and show the toast message
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state?.['showToast']) {
      const message = navigation.extras.state['toastMessage'] || 'Action was successful';
      const title = navigation.extras.state['toastTitle'] || 'Info';
      this.toastr.success(message, title);
    }

    let currentUser = navigation?.extras?.state?.['user'];

    this.user = new User(currentUser.userId, currentUser.email, currentUser.password, currentUser.Person, currentUser.Budget);
    this.budget = this.user.budget;
  }

  loadBudget() {
    return this.budget;
  }

  addExpense() {
    this.budgetsService.addExpense(this.expenseName, this.expenseAmount).subscribe(
      result => {
        this.toastr.success('Your "' + this.expenseName + '" expense has been added!', 'Success!');
      },
      error => {
        this.toastr.error('Unable to add new expense.', 'Error!');
      }
    );
  }

  addIncomeSource() {
    this.budgetsService.addIncomeSource(this.incomeSourceName, this.incomeSourceAmount).subscribe(
      result => {
        this.toastr.success('Income source has been added!', 'Success!');
      },
      error => {
        this.toastr.error('Unable to add new income source.', 'Error!');
      }
    );
  }
}
