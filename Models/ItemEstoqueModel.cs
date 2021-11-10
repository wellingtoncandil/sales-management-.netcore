using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class ItemEstoqueModel
    {
        //os nomes devem estar identicos aos nomes que estão no JSON
   
        public string codigoProduto  { get; set; }
        public string descricaoProduto { get; set; }
        public string qtdeProduto { get; set; }
        public string custo { get; set; }
        public string total { get; set; }

    }
}
