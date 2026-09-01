export class IncomeSource {
  incomeSourceId: string;
  name: string;
  amount: number;
  sortOrder: number;

  constructor(incomeSourceId: string, name: string, amount: number, sortOrder: number) {
    this.incomeSourceId = incomeSourceId;
    this.name = name;
    this.amount = amount;
    this.sortOrder = sortOrder;
  }
}
