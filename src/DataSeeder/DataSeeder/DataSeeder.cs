using DataSeeder.ApplicationDbContext;
using DataSeeder.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DataSeeder;

public static class DataSeeder
{
    private static List<Category> GetCategories()
    {
        return
        [
            new Category { Name = "Motor/Transmisión" },
            new Category { Name = "Frenos" },
            new Category { Name = "Suspensión/Dirección" },
            new Category { Name = "Sistema Eléctrico" },
            new Category { Name = "Mantenimiento" }
        ];
    }

    private static List<Product> GetProducts()
    {
        return
        [
            new Product
            {
                Name = "Bujía de Iridio NGK BKR6EIX-11",
                Description = "Bujía de alto rendimiento para encendido óptimo. Compatible con múltiples modelos.",
                Price = 12.50,
                CategoryId = 1,
                ImagePath = "images/bujia_ngk_bkr6eix.jpg"
            },
            new Product
            {
                Name = "Kit Correa Distribución Gates TCKWP298",
                Description =
                    "Kit completo de correa de distribución con bomba de agua. Esencial para mantenimiento preventivo.",
                Price = 185.75,
                CategoryId = 1,
                ImagePath = "images/kit_distribucion_gates.jpg"
            },
            new Product
            {
                Name = "Pastillas Freno Delanteras Brembo P50068N",
                Description =
                    "Juego de pastillas de freno cerámicas para eje delantero. Alto rendimiento y baja sonoridad.",
                Price = 75.90,
                CategoryId = 2,
                ImagePath = "images/pastillas_freno_brembo.jpg"
            },
            new Product
            {
                Name = "Disco Freno Delantero Bosch 0986479058",
                Description = "Disco de freno ventilado para eje delantero. Calidad OEM para seguridad y durabilidad.",
                Price = 55.00,
                CategoryId = 2,
                ImagePath = "images/disco_freno_bosch.jpg"
            },
            new Product
            {
                Name = "Amortiguador Delantero KYB Excel-G 334604",
                Description =
                    "Amortiguador de gas para eje delantero (lado específico). Restaura el confort y manejo original.",
                Price = 92.30,
                CategoryId = 3,
                ImagePath = "images/amortiguador_kyb.jpg"
            },
            new Product
            {
                Name = "Rótula Suspensión Inferior Moog K80760",
                Description =
                    "Rótula de suspensión para brazo inferior. Componente clave para la estabilidad y dirección.",
                Price = 28.50,
                CategoryId = 3,
                ImagePath = "images/rotula_suspension_moog.jpg"
            },
            new Product
            {
                Name = "Batería Varta Blue Dynamic D24 60Ah 540A",
                Description = "Batería de arranque de 12V, 60Ah y 540A de CCA. Fiabilidad para todo tipo de vehículos.",
                Price = 110.00,
                CategoryId = 4,
                ImagePath = "images/bateria_varta_d24.jpg"
            },
            new Product
            {
                Name = "Alternador Denso DAN930 Remanufacturado",
                Description =
                    "Alternador remanufacturado de alta calidad, 120 Amperios. Garantiza la carga del sistema.",
                Price = 215.40,
                CategoryId = 4,
                ImagePath = "images/alternador_denso.jpg"
            },
            new Product
            {
                Name = "Filtro Aceite Mann-Filter HU 718/5 X",
                Description = "Filtro de aceite ecológico tipo cartucho. Protege el motor de impurezas.",
                Price = 9.80,
                CategoryId = 5,
                ImagePath = "images/filtro_aceite_mann.jpg"
            },
            new Product
            {
                Name = "Aceite Motor Sintético 5W-30 Castrol Edge (5L)",
                Description = "Garrafa de 5 litros de aceite de motor totalmente sintético. Protección avanzada.",
                Price = 48.99,
                CategoryId = 5,
                ImagePath = "images/aceite_castrol_5w30.jpg"
            }
        ];
    }

    public static void SeedProducts(IProductDbContext dbContext)
    {
        if (dbContext.Categories.Any())
        {
            return;
        }

        dbContext.Categories.AddRange(GetCategories());
         ((DbContext)dbContext).SaveChanges();

        dbContext.Products.AddRange(GetProducts());
        ((DbContext)dbContext).SaveChanges();
    }
}