# 🎬 Corte2 - Gestión de Series y Actores

Aplicación de escritorio desarrollada en C# con Windows Forms (.NET 9.0) para la gestión de series, actores y sus relaciones. Permite administrar un catálogo de series y actores, así como asociar actores a series en las que han trabajado.

---

# 👥 Autor

- David Cuartas

---

# ✨ Características principales

## 🎭 Gestión de actores

- Agregar actores
- Editar actores
- Eliminar actores

### Campos disponibles
- Código
- Nombre
- Nacionalidad
- Género
- Fecha de nacimiento
- URL de IMDb

---

## 📺 Gestión de series

- Agregar series
- Editar series
- Eliminar series

### Campos disponibles
- Código
- Nombre
- Año de estreno
- Género
- Temporadas
- URL de IMDb

---

## 🔗 Asociación entre actores y series

- Vincular actores a series
- Desvincular actores
- Visualizar actores asociados a una serie
- Visualizar series relacionadas con un actor

---

## 🖥️ Interfaz gráfica

- Aplicación de escritorio basada en Windows Forms
- Navegación sencilla e intuitiva
- Gestión visual de datos

---

# 🛠️ Tecnologías utilizadas

- C#
- .NET 9.0
- Windows Forms (WinForms)

---

# 📁 Estructura del proyecto

```text
Corte2/
├── Corte2.csproj
├── Program.cs
├── Form1.cs
├── Form1.Designer.cs
│
├── obj/
│   ├── Debug/
│   │   └── net9.0-windows/
│   │       ├── Corte2.AssemblyInfo.cs
│   │       ├── Corte2.GlobalUsings.g.cs
│   │       ├── Corte2.designer.deps.json
│   │       ├── Corte2.designer.runtimeconfig.json
│   │       ├── project.assets.json
│   │       └── ...
│   └── Corte2.csproj.nuget.*
│
├── bin/
│
└── README.md
```

---

# 🚀 Requisitos del sistema

- Windows 7 o superior
- .NET 9.0 Runtime o SDK
- Visual Studio 2022 recomendado

---

# 🔧 Instalación y ejecución

## 1. Clonar el repositorio

```bash
git clone https://github.com/tuusuario/Corte2.git
cd Corte2
```

---

## 2. Abrir el proyecto

Abrir el archivo:

```text
Corte2.csproj
```

en Visual Studio 2022.

---

## 3. Restaurar paquetes

```bash
dotnet restore
```

---

## 4. Compilar el proyecto

```bash
dotnet build
```

---

## 5. Ejecutar la aplicación

```bash
dotnet run
```

También puedes ejecutarlo desde Visual Studio presionando `F5`.

---

# 🎮 Uso de la aplicación

## Gestión de actores

1. Ir a la sección **Actores**
2. Completar los campos requeridos
3. Agregar, editar o eliminar registros

---

## Gestión de series

1. Ir a la sección **Series**
2. Completar los campos requeridos
3. Agregar, editar o eliminar registros

---

## Asociación de actores y series

1. Seleccionar una serie
2. Asociar un actor existente
3. Visualizar relaciones creadas
4. Desvincular actores si es necesario

---

## Visualización de relaciones

- Una serie muestra los actores asociados
- Un actor muestra las series relacionadas

---

# 🧩 Funcionalidades implementadas

| Clase | Métodos principales |
|---|---|
| Form1 | SerieAsocia |
| Form1 | SerieDisocia |
| Form1 | ActorTrabaja |
| Form1 | SerieAdiciona |
| Form1 | ActorAdiciona |
| Form1 | SerieBorra |
| Form1 | ActorBorra |
| Form1 | SerieEdita |
| Form1 | ActorEdita |
| Form1 | ActorEnSerie |
| Form1 | SerieActores |
| Form1 | ObtenerSerie |
| Form1 | ObtenerActor |

---

# 📄 Licencia

Proyecto desarrollado con fines académicos y educativos.
