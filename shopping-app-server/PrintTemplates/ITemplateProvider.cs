
using API.ViewModel;
using System.Threading.Tasks;

namespace API.Abstractions
{
    public interface ITemplateProvider
    {
        Task<string> RenderTemplate(string templateKey, InvoiceViewModel invoice);
    }
}
