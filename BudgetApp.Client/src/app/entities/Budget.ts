import { Expense } from "./Expense";
import { IncomeSource } from "./IncomeSource";

export class Budget {
    budgetId: string;
    expenses: Expense[];
    incomeSources: IncomeSource[];

  constructor(budgetId: string, expenses: Expense[], incomeSources: IncomeSource[]) {
    this.budgetId = budgetId;
    this.expenses = expenses;
    this.incomeSources = incomeSources;
  }
}
