using System.Collections.Generic;
using System.Web.Mvc;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Web.Controllers
{
    public partial class TreeViewController : Controller
    {
      

        public TreeViewController()
        {
        }

        public virtual ActionResult Index()
        {
            List<TreeViewLocation> locations = GetLocations();

            return View();
        }

        public static List<TreeViewLocation> GetLocations()
        {
            var locations = new List<TreeViewLocation>
            {
                new TreeViewLocation
                {
                    Name = "United States",
                    ChildLocations =
                    {
                        new TreeViewLocation
                        {
                            Name = "Chicago",
                            ChildLocations =
                            {
                                new TreeViewLocation {Name = "Rack 1"},
                                new TreeViewLocation {Name = "Rack 2"},
                                new TreeViewLocation {Name = "Rack 3"},
                                new TreeViewLocation {Name = "Rack 3"},
                            }
                        },
                        new TreeViewLocation
                        {
                            Name = "Dallas",
                            ChildLocations =
                            {
                                new TreeViewLocation
                                {
                                    Name = "Rack 1",
                                    ChildLocations =
                                    {
                                        new TreeViewLocation
                                        {
                                            Name = "Rack 1",
                                            ChildLocations =
                                            {
                                                new TreeViewLocation {Name = "Rack 1"},
                                                new TreeViewLocation {Name = "Rack 2"},
                                                new TreeViewLocation {Name = "Rack 3"},
                                                new TreeViewLocation {Name = "Rack 3"},
                                            }
                                        },
                                        new TreeViewLocation {Name = "Rack 2"},
                                        new TreeViewLocation {Name = "Rack 3"},
                                        new TreeViewLocation {Name = "Rack 3"},
                                    }
                                },
                                new TreeViewLocation {Name = "Rack 2"},
                                new TreeViewLocation {Name = "Rack 3"},
                                new TreeViewLocation {Name = "Rack 3"},
                            }
                        },
                        new TreeViewLocation {Name = "Dallas"},
                        new TreeViewLocation {Name = "Dallas"},
                        new TreeViewLocation {Name = "Dallas"},
                        new TreeViewLocation {Name = "Dallas"},
                        new TreeViewLocation {Name = "Dallas"},
                        new TreeViewLocation {Name = "Dallas"},
                    }
                },
                new TreeViewLocation
                {
                    Name = "Canada",
                    ChildLocations =
                    {
                        new TreeViewLocation {Name = "Ontario"},
                        new TreeViewLocation {Name = "Windsor"}
                    }
                }
            };
            return locations;
        }
    }

    public class TreeViewLocation
    {
        public TreeViewLocation()
        {
            ChildLocations = new HashSet<TreeViewLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TreeViewLocation> ChildLocations { get; set; }
    }
}