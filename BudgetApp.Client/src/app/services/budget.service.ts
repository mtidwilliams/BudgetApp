import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Budget } from '../entities/Budget';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private apiUrl = '/budget';

  constructor(private http: HttpClient) { }

  getCurrentBudget(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/getCurrentBudget`, {});
  }

  addExpense(expenseName: string, expenseAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/addExpense`, { expenseName, expenseAmount });
  }

  updateExpense(expenseId: string, expenseName: string, expenseAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/updateExpense`, { expenseId, expenseName, expenseAmount });
  }

  removeExpense(expenseId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/removeExpense?expenseId=${expenseId}`, {});
  }

  addIncomeSource(incomeSourceName: string, incomeSourceAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/addIncomeSource`, { incomeSourceName, incomeSourceAmount });
  }

  updateIncomeSource(incomeSourceId: string, incomeSourceName: string, incomeSourceAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/updateIncomeSource`, { incomeSourceId, incomeSourceName, incomeSourceAmount });
  }

  removeIncomeSource(incomeSourceId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/removeIncomeSource?incomeSourceId=${incomeSourceId}`,  {});
  }

  saveOrientation(budget: Budget) : Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/saveBudgetOrientation`, { budget });
  }
}
