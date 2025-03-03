namespace ETicaretAPI.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}


// CQRS patternda request nesnelerimizin karşılığı ViewModel'dir.

// Validasyon , ViewModele ya da CQRS patternda request nesnelerine karşı bire bir oluşturulacak nesnelerdir.Her biri için ayrı ayrı oluşturmak en doğrusudur.Create ayrı update ayrı.