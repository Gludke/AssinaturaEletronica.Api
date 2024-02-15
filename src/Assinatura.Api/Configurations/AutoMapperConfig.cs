using AutoMapper;

namespace Proj.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Fornecedor, AddFornecedorViewModel>().ReverseMap();
            
            //CreateMap<Produto, ProdutoViewModel>()//pegando o nome do 'Fornecedor' e mapeando dentro da var de 'ProdutoViewModel'
            //    .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(prod => prod.Fornecedor.Nome));



        }
    }
}
