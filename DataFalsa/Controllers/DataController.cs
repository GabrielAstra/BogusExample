using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace DataFalsa.Controllers
{
    public class DataController : ControllerBase
    {
        public class Produto
        {
            public string nome { get; set; }
            public string descricao { get; set; }
            public decimal preco { get; set; }
        }

        private static List<Produto> ListaProdutos = new List<Produto>();

        [HttpGet("produto")]
        public ActionResult<IEnumerable<Produto>> Produtos()
        {
            if (ListaProdutos.Count == 0)
            {
                var faker = new Faker<Produto>("pt_BR").RuleFor(p => p.nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.descricao, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.preco, f => decimal.Parse(f.Commerce.Price()));


                ListaProdutos = faker.Generate(5);
            }

            return Ok(ListaProdutos);


        }

        [HttpGet("produtos/filtrarpreco")]
        public ActionResult<IEnumerable<Produto>> FiltrarPreco([FromQuery] decimal? precoFitlrado = null)
        {
            if(ListaProdutos.Count == 0)
            {
                var faker = new Faker<Produto>("pt_BR").RuleFor(p => p.nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.descricao, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.preco, f => decimal.Parse(f.Commerce.Price()));


                ListaProdutos = faker.Generate(5);
            }
            

            var produtoFiltrado = ListaProdutos;
            if(precoFitlrado.HasValue)
            {
                produtoFiltrado = produtoFiltrado.Where(p => p.preco == precoFitlrado).ToList();

            }

            return Ok(produtoFiltrado);
        }
    }
}
