using Jazb.Model;
using Jazb.Model.AdminModel;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IOptionService
    {
        bool ModeratingComment { get; }
        SiteConfig GetAll();
        void Update(UpdateOptionModel model);
    }
}