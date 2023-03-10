# 馃挵 Alkemy Wallet Backend Core Web API
## 馃殌 Programa .NET eFALCOM by Alkemy
*Practica personal del proyecto realizado con el team la Dotneta [AlkemyWallet](https://github.com/emmanuelranone/AlkemyWallet/tree/dev)*
*Implementando todas las funcionalidades b谩sicas que un usuario necesita para usar una billetera virtual*

*馃搶 Requerimientos:*
- Iniciar sesi贸n.
- Realiza dep贸sitos en su cuenta.
- Realizar transferencias a otras cuentas.
- Actualizar y visualizar sus datos.
- Crear y eliminar usuarios.
- Actualizar y visualizar los datos de otros usuarios.
- Actualizar, listar y eliminar transacciones realizadas por otros usuarios.

*馃摎 Requerimientos t茅cnicos:*
- Uso de Entity Framework Core utilizando Code First.
- Uso del patr贸n Unit of Work.
- Manejo de sesiones usando JWT (JSON Web Tokens).
- Manejo de errores.
- Documentaci贸n y pruebas de la API con Swagger.

*馃 Caracter铆sticas de la API:*
- **Roles de Usuario:** Tendremos dos roles principales, uno para los usuarios
no administradores que utilizar谩n los servicios de la wallet y otro para los
administradores que necesiten gestionar al rol anterior.
- **Account:** Es la cuenta bancaria del usuario. Nos devuelve el dinero
disponible en la cuenta y tambi茅n tiene m茅todos para dep贸sitos y
transferencias.
- **Transacci贸n:** Historial de movimientos de la cuenta de un usuario.

*馃洜锔? El proyecto est谩 desarrollado con:*
* Lenguaje C#
* .NET Core 6
* Fluent API
* AutoMapper

## 馃彈锔? **Arquitectura**
<details>
  <summary>Especificaci贸n de Capas</summary>
  
  ### **Entities**
  En este nivel de la arquitectura definiremos todas las entidades de la base de datos.
  
  ### **DataAccess**
  Es donde definiremos el WalletDbContext y crearemos los seeds correspondientes para popular nuestras entidades.
  
  ### **Controller**
  Ser谩 el punto de entrada a la API. En los controladores deber铆amos definir la menor cantidad de l贸gica posible y utilizarlos como un pasamanos con la capa de servicios.
  
  ### **Services**
  Se incluir谩n todas las Interfaces de servicios y sus implementaciones.
  
  ### **Repositories**
  En esta capa definiremos las clases correspondientes para realizar el Repository Pattern y Unit of Work
  
  ### **Core**
  Es nuestra capa principal, en ella encontramos varios subniveles
  
  *	Helper: Definiremos l贸gica que pueda ser de utilidad para todo el proyecto. Por ejemplo, alg煤n m茅todo para encriptar/desencriptar contrase帽as
  *	Mapper: En esta carpeta ir谩n las clases de mapeo para vincular entidad-dto o dto-entidad
  *	Models: se definir谩n los modelos que necesitemos para el desarrollo. Dentro de esta carpeta encontramos DTO tanto para realizar input como output.
</details>
