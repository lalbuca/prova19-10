//Prova dia 19/10/2026 

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<Folha> folhas = new List<Folha>();

//Funcionalidade 1- Cadasto da folha de pagamento
//POST: /api/folha/cadastrar
app.MapPost("/api/folha/cadastrar", (Folha? folha) =>
{
    if (folha is null)
    {
        return Results.BadRequest("A folha de pagamento não pode ser nula");
    }

    if( 1 > folha.Mes || folha.Mes > 12  )
    {
        return Results.BadRequest("O mês precisa ser um valor entre 1 e 12");
    }

    if(folha.Ano < 2000)
    {
        return Results.BadRequest("O Ano preisa ser maior ou igual a 2000");
    }

    foreach (Folha folhasCadastradas in folhas)
    {
        if (folhasCadastradas.Cpf == folha.Cpf)
        {
            return Results.BadRequest("Já existe uma folha deste cpf");
        }

        if(folhasCadastradas.Mes == folha.Mes)
        {
            return Results.BadRequest("Já existe uma folha deste mês");
        }

        if(folhasCadastradas.Ano == folha.Ano)
        {
            return Results.BadRequest("Já existe uma folha deste ano");
        }
    }
    folhas.Add(folha);
    return Results.Created("", folha);

});

//Funcionalidade 2 - Listar Folhas Cadastradas
app.MapGet("/api/folha/listar", () =>
{
    if (folhas.Count == 0)
    {
        return Results.BadRequest("A lista de folhas está vazia");
    }
    return Results.Ok(folhas);
});

//Funcionalidade 3 - Buscar folha por CPF Mes e Ano
app.MapGet("/api/produto/buscar/{cpf}/{mes}/{ano}", (string cpf, int mes, int ano) =>
{

    Folha? resultado = folhas.FirstOrDefault(x => x.Cpf == cpf);
    if(resultado is null)
    {
        return Results.NotFound("Folha não encontrada!");
    }
    return Results.Ok(resultado);

});

//Funcionalidade 4 – Remoção de Folha por CPF, Mês e Ano

app.MapDelete("/api/produto/remover/{cpf}/{mes}/{ano}", (string cpf, int mes, int ano) =>
{
    //Expressão Lambda
    Folha? resultado = folhas.FirstOrDefault(x => x.Cpf == cpf);
    if(resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    folhas.Remove(resultado);
    return Results.Ok(resultado);
});

//Funcionalidade 5 – Total Líquido de Todas as Folhas


app.Run();