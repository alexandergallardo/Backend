# AGAVAL : Prueba Tecnica. Mi primera experiencia con webapi en C# y Entity Framework Core

## CONSTRUCCIÓN MODELOS

- Cliente
- Usuario
- Producto ( inventario de productos para tener control de stock)
- OrdenCompra
- OrdenCompraItems 

## MIGRAR MODELOS
- dotnet ef migrations add MigracionInicial (Si no existe la carpeta Migrations, usar este comando para generarlas)
- dotnet ef database update  ( Llevar el modelo al motor d ebase de datos sql server)

## TAREAS REALIZADAS

- Proceso de conexión ala BD a traves de Entity Framework Core
- JWT para asegurar las api
- Swagger para documentacion de API
- Uso de Inyección de Dependencias

## TAREAS PENDIENTES
- Pendiente por ajustar: AL crear orden de comrpa validar que exista el cliente
- Al crar Item de la orden d ecompra, validar que el producto exista
- Pendiente desarrollar Front End en Angular
- Pendiente desplegar en AZURE o AWS
