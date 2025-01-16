import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BudgetsService {
  private apiUrl = '/budget';

  constructor(private http: HttpClient) { }

  addExpense(expenseName: string, expenseAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/addExpense`, { expenseName, expenseAmount });
  }

  removeExpense(expenseId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/removeExpense`, { expenseId });
  }

  addIncomeSource(incomeSourceName: string, incomeSourceAmount: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/addIncomeSource`, { incomeSourceName, incomeSourceAmount });
  }

  removeIncomeSource(incomeSourceId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/removeIncomeSource`, { incomeSourceId });
  }
}
