# 💰 Alkemy Wallet Backend Core Web API
## 🚀Programa .NET eFALCOM by Alkemy
*Practica personal del proyecto realizado con el team la Dotneta [AlkemyWallet](https://github.com/emmanuelranone/AlkemyWallet/tree/dev)*
*Implementando todas las funcionalidades básicas que un usuario necesita para usar una billetera virtual*

*📌 Requerimientos:*
- Iniciar sesión.
- Realiza depósitos en su cuenta.
- Realizar transferencias a otras cuentas.
- Actualizar y visualizar sus datos.
- Crear y eliminar usuarios.
- Actualizar y visualizar los datos de otros usuarios.
- Actualizar, listar y eliminar transacciones realizadas por otros usuarios.

*📚 Requerimientos técnicos:*
- Uso de Entity Framework Core utilizando Code First.
- Uso del patrón Unit of Work.
- Manejo de sesiones usando JWT (JSON Web Tokens).
- Manejo de errores.
- Documentación y pruebas de la API con Swagger.

*🛠️ El proyecto está desarrollado con:*
- Lenguaje C#
- .NET Core 6
- Fluent API
- AutoMapper

## 🏗️ **Especificación de la Arquitectura**

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
