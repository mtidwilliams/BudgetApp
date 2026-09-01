export class Expense {
  expenseId: string;
  name: string;
  amount: number;
  sortOrder: number;

  constructor(expenseId: string, name: string, amount: number, sortOrder: number) {
    this.expenseId = expenseId;
    this.name = name;
    this.amount = amount;
    this.sortOrder = sortOrder;
  }
}
