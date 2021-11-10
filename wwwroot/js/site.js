$(function () {
    $("#pesquisa input").keyup(function () {
        var nth = "#tabela td:nth-child(4)"; //4 é o numero da coluna
        var valor = $(this).val().toUpperCase();
        $("#tabela tbody").show();
        $(nth).each(function () {
            var resultado = $(this).text().toUpperCase().indexOf(valor);
            if (resultado < 0) {
                $(this).parent().hide();
            } else {
                $(this).parent().show();
            }
        });
    });
    $("#pesquisa input").blur(function () {
        $(this).val("");
        $("#tabela tbody").show();
    });
});

/*Primeiramente, programamos o evento keyup dos inputs existentes na tabela para que, quando o usuário pressionar uma tecla estando com o cursor do mouse sobre um dos campos de texto, os registros da tabela serão filtrados a partir da coluna em que se efetuou o filtro e o valor que foi digitado.

Vejamos a explicação detalhada de cada linha do código acima:

3. A variável “index” receberá como valor, a coluna que contém o input que invocou o evento. Para recuperar este objeto, foi utilizado o método parent() da biblioteca jQuery.

4. A variável “nth” é apenas um string para ser usado posteriormente na seleção da coluna filtrada em todas as linhas da tabela. Por exemplo, ao filtrar
a primeira coluna, seu conteúdo deverá ser “#tabela td:nth-child(1)”, ou seja, um seletor CSS.

5. A variável “valor” receberá o conteúdo o input que está sendo utilizado para fazer o filtro, convertendo o texto para maiúsculo.
Essa conversão é feita para que a consulta seja “case insensitive”, ou seja, não diferencie letras maiúsculas de minúsculas.

6. Para iniciar o filtro, todas as linhas são exibidas inicialmente para serem ocultadas depois, se for o caso.

7. Utilizamos a função each() da jQuery para realizar uma ação para cada coluna filtrada pelo seletor definido anteriormente pela variável “nth”.

8-9. Caso a coluna filtrada contenha o texto digitado, a linha que a contém é ocultada. Para isso usamos novamente a função parent() para recuperar a
tag TR que contém a tag TD utilizada para a verificação. A existência ou não do texto digitado na coluna nos é informada pela função indexOf() do Javascript,
que retornará -1 se o valor informado não estiver contido no texto alvo. Assim, se a coluna não contém o texto digitado, a linha que a contém é ocultada. Vale
notar que convertemos também o conteúdo da célula para maiúsculo, para não haver diferenciação na hora do filtro.

13-14. Nessas linhas, programamos o evento blur dos inputs para que seu conteúdo seja limpo ao perderem o foco.

*/
