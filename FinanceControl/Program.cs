using FinanceControl.Data;
using FinanceControl.Models;
using FinanceControl.Services;

Database.Initialize();
var service = new TransactionService();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Controle Financeiro Pessoal ===");
    Console.WriteLine("1 - Adicionar Receita");
    Console.WriteLine("2 - Adicionar Despesa");
    Console.WriteLine("3 - Listar Transações");
    Console.WriteLine("4 - Ver Saldo");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            AddTransaction("Receita");
            break;
        case "2":
            AddTransaction("Despesa");
            break;
        case "3":
            ListTransactions();
            break;
        case "4":
            ShowBalance();
            break;
        case "0":
            return;
    }
}

void AddTransaction(string type)
{
    Console.Write("Descrição: ");
    var desc = Console.ReadLine() ?? "";

    Console.Write("Valor: ");
    double.TryParse(Console.ReadLine(), out double value);

    service.AddTransaction(new Transaction
    {
        Type = type,
        Description = desc,
        Value = value
    });

    Console.WriteLine($"{type} adicionada com sucesso!");
    Console.ReadKey();
}

void ListTransactions()
{
    var transactions = service.GetAll();
    foreach (var t in transactions)
        Console.WriteLine($"{t.Id} | {t.Type} | {t.Description} | R$ {t.Value:F2} | {t.Date:dd/MM/yyyy}");
    Console.ReadKey();
}

void ShowBalance()
{
    Console.WriteLine($"Saldo atual: R$ {service.GetBalance():F2}");
    Console.ReadKey();
}
