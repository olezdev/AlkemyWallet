# Alkemy Wallet Backend Core Web API
*El proyecto está desarrollado con:*
- Language C#
- .NET Core 6
- Entity Framework Core
- Code First
- Fluent API
- AutoMapper
- Swagger

## Programa .NET eFALCOM by Alkemy
*Practica personal del proyecto realizado con el team la Dotneta [AlkemyWallet](https://github.com/emmanuelranone/AlkemyWallet/tree/dev)*

## **Especificación de la Arquitectura**

### **Capa Entities**
En este nivel de la arquitectura definiremos todas las entidades de la base de datos.

### **Capa DataAccess**
Es donde definiremos el WalletDbContext y crearemos los seeds correspondientes para popular nuestras entidades.

### **Capa Controller**
Será el punto de entrada a la API. En los controladores deberíamos definir la menor cantidad de lógica posible y utilizarlos como un pasamanos con la capa de servicios.

### **Capa Services**
Se incluirán todas las Interfaces de servicios y sus implementaciones.

### **Capa Repositories**
En esta capa definiremos las clases correspondientes para realizar el Repository Pattern y Unit of Work

### **Capa Core**
Es nuestra capa principal, en ella encontramos varios subniveles

*	Helper: Definiremos lógica que pueda ser de utilidad para todo el proyecto. Por ejemplo, algún método para encriptar/desencriptar contraseñas
*	Mapper: En esta carpeta irán las clases de mapeo para vincular entidad-dto o dto-entidad
*	Models: se definirán los modelos que necesitemos para el desarrollo. Dentro de esta carpeta encontramos DTO tanto para realizar input como output.