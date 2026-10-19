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


app.Run();