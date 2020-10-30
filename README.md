# TakeReposBot
 Teste Take Blip
 
Foi construído uma API em .NET Core. Para fazer consulta no Git da Take e trazer as informações sobre os 5 repositórios de linguagem C# mais antigos da Take, ordenados de forma crescente por data de criação.

Essa API foi publicada no Azure, sendo possível ser acessada pelo link abaixo:
https://takegitrepositories20201029011156.azurewebsites.net/Api/TakeGitRepostories

Dentro da Pasta TakeReposBot/TakeGitRepositories/ é possível acessar o arquivo texto contendo o json do bot - TakeReposBot_Json.txt



# Step by Step

1) Foi criado o método ProcessRepositories() que faz a busca na API do Git e retorna o json com todos os dados.

2) Foi criado uma o arquivo Repos, utilizando "Paste Special Past JSON as Classes" foi criado as classes correspontes com o retorno do JSON, exemplo: Rootobject, Owne, Permissions.

3) Foi criado o método  FirstFiveRepository() que retorna as informações sobre os 5 repositórios de linguagem C# mais antigos da Take, ordenados de forma crescente por data de criação.

4) Foi criado o método PopulateObjectForCarousel() entrega o JSON necessário para a construição do carrosel. Também foi criado outro método chamado PopulateObjectForCarouselUsingString() que retorna o mesmo JSON, mas de uma maneira diferente.

5) Get() é a API que retorna a requisição HTTP

6) O Bot foi construido no https://portal.blip.ai/ com o nome de takereposbot. O arquivo texto com o json do bot pode ser acesso no diretorio TakeReposBot/TakeGitRepositories/TakeReposBot_Json.txt

7) Resultado:

![image](https://drive.google.com/uc?export=view&id=1PaKfjbplaTC2H5sGiDwkUQZUKN5x8N9C)

# How to run

Para rodar o projeto basta clonar o repository, e abrir o TakeGitRepositories.sln com o Visual Studio e executar a aplicação e o endereço parecido deve aparecer na URL:
 https://localhost:44365/Api/TakeGitRepostories com o json como resultado.  





