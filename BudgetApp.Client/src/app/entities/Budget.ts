import { Expense } from "./Expense";
import { IncomeSource } from "./IncomeSource";

export class Budget {
    budgetId: string;
    expenses: Expense[];
    income: IncomeSource[];

  constructor(budgetId: string, expenses: Expense[], income: IncomeSource[]) {
    this.budgetId = budgetId;
    this.expenses = expenses;
    this.income = income;
  }
}
