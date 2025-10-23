
# Proyecto veterinaria

Con esta api trartamos  dar una solucion para controlar el flujo de trabajo en una veterinaria


## Autores

- Rolando Abimael Chompa Guzman
- Luis Angel Diaz Diaz


## Instalación

Instalacion de proyecto usando git usando SSH

```bash
  git clone git@github.com:proyecto-chat/Veterinaria_Equipo_GuzDiaz.git
  
```
Instalacion de proyecto usando git usando HTTPS

```bash
  git clone https://github.com/proyecto-chat/Veterinaria_Equipo_GuzDiaz.git
```

## Ejecutar el proyecto

```bash
  cd Veterinaria_Equipo_GuzDiaz
  dotnet watch run
```

# Endpoints

## DUEÑO DE LAS MASCOTAS

#### Registrar un dueño
```http
  POST /veterinaria/dueños
```

El endpoint espera un JSON con la siguiente estructura:

```json
{
  "nombre": "string",
  "apellido": "string",
  "edad": 0,
  "telefono": "string",
  "dni": "string",
  "direccion": "string"
}
```

#### Obtener toodos los dueños
```http
  GET /veterinaria/dueños
```
El endpoint devuevle una lista con todos los clientes que existen en la base de datos

#### Buscar aun dueño en especifico
```http
  GET /veterinaria/dueños/buscar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `idDueño` | string | **Required**. |

#### Actualizar la informacion de un dueño en especifico
```http
  PUT` /veterinaria/dueños/actualizar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `idDueño` | string | **Required**

El endpoint espera un JSON con la siguiente estructura:

```json
{
  "nombre": "string",
  "apellido": "string",
  "edad": 0,
  "telefono": "string",
  "dni": "string",
  "direccion": "string"
}
```

#### Eliminar a un duño de la veterinaria
```http
  DELETE` /veterinaria/dueños/eliminar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `idDueño` | string | **Required**

#### Obtener las mascotas de un dueño
```http
GET` /veterinaria/dueños/mascotas
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `dni` | string | **Required**

# Mascotas
```http
POST /veterinaria/mascotas
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `dni` | string | **Required**

El endpoint espera un JSON con la siguiente estructura:

```json
{
  "nombre": "string",
  "edad": 0,
  "peso": 0,
  "especie": {
    "nombreEspecie": "string",
    "raza": "string"
  }
}
```

```http
GET /veterinaria/mascotas
```
El endpoint devuevle una lista con todos las mascotas que existen en la base de datos

```http 
GET /veterinaria/mascotas/buscar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

```http 
PUT /veterinaria/mascotas/actualizar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

El endpoint espera un JSON con la siguiente estructura:

``` json
{
  "nombre": "string",
  "edad": 0,
  "peso": 0
}
```
### Eliminar una mascota
```http   
DELETE /veterinaria/mascotas/eliminar
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

# Servicios
### Crear un nuevo servicio
```http  
POST /veterinaria/servicios
```
El endpoint espera un JSON con la siguiente estructura:
```json
{
  "tiposServicio": [
    "string"
  ],
  "descripcion": "string",
  "costo": 0,
  "veterinarioId": "string",
  "mascotaId": "string"
}
```
#### Obtener todos los servicios de la base de datos

```http  
GET /veterinaria/servicios
```
El endpoint devuelve una lista de todos los servicios que se hay realizado

#### Obtiene la informacion de un servicio en especifico

```http  
GET` /veterinaria/servicios/
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

#### Actualizar la inforamcion de un servicio

```http  
 PUT /veterinaria/servicios/
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

El endpoint espera un JSON con la siguiente estructura:
```json
{
  
}
```
#### Eliminar un servicio especifico

```http 
 DELETE /veterinaria/servicios/
```

| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**


# Veterinarios

#### Registrar a un nuevo veterinario

```http 
POST /veterinaria/veterinarios
```

El endpoint espera un JSON con la siguiente estructura:
```json
{
  "nombre": "string",
  "apellido": "string",
  "edad": 0,
  "telefono": "string",
  "dni": "string",
  "direccion": "string",
  "matricula": "string",
  "especialidades": [
    {
      "nombre": "string",
      "descripcion": "string"
    }
  ]
}
```
#### Obtienen a todos los veterinarios en la base de datos
```http
GET /veterinaria/veterinarios
```
Este endpoint devuelve una lista con todos los veterianrios registrados en la base de datos


#### Obtener la información de un veterianrio especifico

```http
GET /veterinaria/veterinarios/
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

#### Actualizar la informacion de un veterianrio especifico

```http
PUT` /veterinaria/veterinarios/{id}
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

```json
{
  "matricula": "string",
  "nombre": "string",
  "apellido": "string",
  "telefono": "string",
  "direccion": "string",
  "edad": 0
}
```

```http
DELETE /veterinaria/veterinarios/
```
| Parameter | Type   | Description              |
|-----------|--------|--------------------------|
| `id` | string | **Required**

