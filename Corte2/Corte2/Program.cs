using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Corte2
{
    // MODEL CLASSES
    public class ActorActriz
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Codigo { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string URLIMDB { get; set; } = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DateTime FechaNacimiento { get; set; }

        public string Nacionalidad { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        public ActorActriz(int codigo, string nombre, string urlImdb, DateTime fechaNacimiento, string nacionalidad, string genero)
        {
            Codigo = codigo;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            URLIMDB = "https://www.imdb.com/name/" + (urlImdb ?? throw new ArgumentNullException(nameof(urlImdb)));
            FechaNacimiento = fechaNacimiento;
            Nacionalidad = nacionalidad ?? throw new ArgumentNullException(nameof(nacionalidad));
            Genero = genero ?? throw new ArgumentNullException(nameof(genero));
        }
    }

    public class Serie
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Codigo { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string URLIMDB { get; set; } = string.Empty;
        public int AnioEstreno { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int Temporadas { get; set; }
        public List<int> Actores { get; set; } = new List<int>();

        public Serie(int codigo, string nombre, string urlImdb, int anioEstreno, string genero, int temporadas)
        {
            Codigo = codigo;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            URLIMDB = "https://www.imdb.com/title/" + (urlImdb ?? throw new ArgumentNullException(nameof(urlImdb)));
            AnioEstreno = anioEstreno;
            Genero = genero ?? throw new ArgumentNullException(nameof(genero));
            Temporadas = temporadas;
        }
    }

    // DATA PERSISTENCE
    public class Persistencia
    {
        public List<ActorActriz> Actores { get; set; } = new List<ActorActriz>();
        public List<Serie> Series { get; set; } = new List<Serie>();

        public Persistencia()
        {
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            // Sample actors
            ActorAdiciona(101, "Ana María Orozco", "nm0650450", new DateTime(1973, 7, 4), "Colombiana", "Femenino");
            ActorAdiciona(102, "Laura Londoño", "nm2256810", new DateTime(1988, 11, 22), "Colombiana", "Femenino");
            ActorAdiciona(103, "Carolina Ramírez", "nm1329835", new DateTime(1983, 12, 25), "Colombiana", "Femenino");
            ActorAdiciona(104, "Catherine Siachoque", "nm0796171", new DateTime(1972, 1, 21), "Venezolana", "Femenino");
            ActorAdiciona(105, "Carmenza González", "nm1863990", new DateTime(1965, 5, 15), "Colombiana", "Femenino");
            ActorAdiciona(106, "Andrés Londoño", "nm2150265", new DateTime(1985, 3, 18), "Colombiano", "Masculino");
            ActorAdiciona(107, "Sebastián Martínez", "nm1234567", new DateTime(1980, 8, 10), "Colombiano", "Masculino");
            ActorAdiciona(108, "Valeria Emiliani", "nm7654321", new DateTime(1990, 4, 5), "Colombiana", "Femenino");
            ActorAdiciona(109, "Juan Pablo Raba", "nm9876543", new DateTime(1977, 1, 14), "Colombiano", "Masculino");
            ActorAdiciona(110, "Majida Issa", "nm2468101", new DateTime(1982, 9, 30), "Colombiana", "Femenino");

            // Sample series
            SerieAdiciona(201, "Yo soy Betty, la fea", "tt0233127", 1999, "Telenovela", 1);
            SerieAdiciona(202, "La reina del flow", "tt8560918", 2018, "Drama musical", 2);
            SerieAdiciona(203, "Café con Aroma de Mujer", "tt14471346", 2021, "Telenovela", 1);
            SerieAdiciona(204, "Los Briceño", "tt10348478", 2021, "Comedia", 1);
            SerieAdiciona(205, "Distrito Salvaje", "tt8105958", 2019, "Acción", 2);
            SerieAdiciona(206, "Mil Colmillos", "tt9701670", 2020, "Drama", 1);
            SerieAdiciona(207, "Perdida", "tt10064124", 2020, "Drama", 1);
            SerieAdiciona(208, "El Cartel de los Sapos", "tt1332705", 2008, "Narcoserie", 2);
            SerieAdiciona(209, "Escobar, el patrón del mal", "tt2262532", 2012, "Narcoserie", 1);
            SerieAdiciona(210, "La Nieta Elegida", "tt1123456", 2022, "Telenovela", 1);

            // Actor-Series relationships
            SerieAsocia(201, 101); SerieAsocia(201, 104); SerieAsocia(201, 105);
            SerieAsocia(202, 102); SerieAsocia(202, 103);
            SerieAsocia(203, 101); SerieAsocia(203, 110);
            SerieAsocia(204, 106); SerieAsocia(204, 107);
            SerieAsocia(205, 101); SerieAsocia(205, 109);
            SerieAsocia(206, 108);
            SerieAsocia(207, 101); SerieAsocia(207, 104);
            SerieAsocia(208, 109);
            SerieAsocia(209, 109);
            SerieAsocia(210, 110);
        }

        // ACTOR CRUD METHODS
        public bool ActorAdiciona(int codigo, string nombre, string urlImdb, DateTime fechaNacimiento, string nacionalidad, string genero)
        {
            if (Actores.Any(a => a.Codigo == codigo)) return false;
            Actores.Add(new ActorActriz(codigo, nombre, urlImdb, fechaNacimiento, nacionalidad, genero));
            return true;
        }

        public bool ActorEdita(int codigo, string nombre, string urlImdb, DateTime fechaNacimiento, string nacionalidad, string genero)
        {
            var actor = Actores.FirstOrDefault(a => a.Codigo == codigo);
            if (actor == null) return false;

            actor.Nombre = nombre;
            actor.URLIMDB = urlImdb;
            actor.FechaNacimiento = fechaNacimiento;
            actor.Nacionalidad = nacionalidad;
            actor.Genero = genero;
            return true;
        }

        public bool ActorBorra(int codigo)
        {
            if (ActorEnSerie(codigo)) return false;
            var actor = Actores.FirstOrDefault(a => a.Codigo == codigo);
            return actor != null && Actores.Remove(actor);
        }

        public bool ActorEnSerie(int codigoActor)
        {
            return Series.Any(s => s.Actores.Contains(codigoActor));
        }

        public List<string> ActorTrabaja(int codigoActor)
        {
            return Series.Where(s => s.Actores.Contains(codigoActor))
                        .Select(s => s.Nombre)
                        .ToList();
        }

        public ActorActriz? ObtenerActor(int codigo)
        {
            return Actores.FirstOrDefault(a => a.Codigo == codigo);
        }

        // SERIES CRUD METHODS
        public bool SerieAdiciona(int codigo, string nombre, string urlImdb, int anioEstreno, string genero, int temporadas)
        {
            if (Series.Any(s => s.Codigo == codigo)) return false;
            Series.Add(new Serie(codigo, nombre, urlImdb, anioEstreno, genero, temporadas));
            return true;
        }

        public bool SerieEdita(int codigo, string nombre, string urlImdb, int anioEstreno, string genero, int temporadas)
        {
            var serie = Series.FirstOrDefault(s => s.Codigo == codigo);
            if (serie == null) return false;

            serie.Nombre = nombre;
            serie.URLIMDB = urlImdb;
            serie.AnioEstreno = anioEstreno;
            serie.Genero = genero;
            serie.Temporadas = temporadas;
            return true;
        }

        public bool SerieBorra(int codigo)
        {
            var serie = Series.FirstOrDefault(s => s.Codigo == codigo);
            return serie != null && Series.Remove(serie);
        }

        public List<string> SerieActores(int codigoSerie)
        {
            var serie = Series.FirstOrDefault(s => s.Codigo == codigoSerie);
            if (serie == null) return new List<string>();

            return serie.Actores.Select(codigoActor =>
            {
                var actor = ObtenerActor(codigoActor);
                return actor != null ? $"[{actor.Codigo}] {actor.Nombre}" : "Actor no encontrado";
            }).ToList();
        }

        public bool SerieAsocia(int codigoSerie, int codigoActor)
        {
            var serie = Series.FirstOrDefault(s => s.Codigo == codigoSerie);
            if (serie == null || serie.Actores.Contains(codigoActor)) return false;

            serie.Actores.Add(codigoActor);
            return true;
        }

        public bool SerieDisocia(int codigoSerie, int codigoActor)
        {
            var serie = Series.FirstOrDefault(s => s.Codigo == codigoSerie);
            return serie != null && serie.Actores.Remove(codigoActor);
        }

        public Serie? ObtenerSerie(int codigo)
        {
            return Series.FirstOrDefault(s => s.Codigo == codigo);
        }
    }

    // FORMS
    public class MainForm : Form
    {
        private readonly Persistencia _persistencia;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem actoresToolStripMenuItem;
        private ToolStripMenuItem seriesToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private Label lblTitulo;

        public MainForm(Persistencia persistencia)
        {
            _persistencia = persistencia;
            InitializeComponent();
            Text = "Software TV Show";
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new MenuStrip();
            this.actoresToolStripMenuItem = new ToolStripMenuItem();
            this.seriesToolStripMenuItem = new ToolStripMenuItem();
            this.salirToolStripMenuItem = new ToolStripMenuItem();
            this.lblTitulo = new Label();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.actoresToolStripMenuItem,
                this.seriesToolStripMenuItem,
                this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);

            // actoresToolStripMenuItem
            this.actoresToolStripMenuItem.Text = "Actores";
            this.actoresToolStripMenuItem.Click += (s, e) => {
                new ActoresForm(_persistencia).ShowDialog();
            };

            // seriesToolStripMenuItem
            this.seriesToolStripMenuItem.Text = "Series";
            this.seriesToolStripMenuItem.Click += (s, e) => {
                new SeriesForm(_persistencia).ShowDialog();
            };

            // salirToolStripMenuItem
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += (s, e) => Close();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 40);
            this.lblTitulo.Text = "Software TV Show";

            // MainForm
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }

    public class ActoresForm : Form
    {
        private readonly Persistencia _persistencia;
        private DataGridView dgvActores;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnBorrar;
        private Button btnSeries;

        public ActoresForm(Persistencia persistencia)
        {
            _persistencia = persistencia;
            InitializeComponent();
            CargarActores();
        }

        private void InitializeComponent()
        {
            this.dgvActores = new DataGridView();
            this.btnAgregar = new Button();
            this.btnEditar = new Button();
            this.btnBorrar = new Button();
            this.btnSeries = new Button();

            // dgvActores
            this.dgvActores.Dock = DockStyle.Top;
            this.dgvActores.Height = 250;
            this.dgvActores.ReadOnly = true;
            this.dgvActores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // btnAgregar
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Top = 260;
            this.btnAgregar.Left = 20;
            this.btnAgregar.Click += (s, e) => {
                var form = new EditarActorForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!_persistencia.ActorAdiciona(form.Codigo, form.Nombre, form.URLIMDB,
                        form.FechaNacimiento, form.Nacionalidad, form.Genero))
                    {
                        MessageBox.Show("Error: El código ya existe");
                    }
                    CargarActores();
                }
            };

            // btnEditar
            this.btnEditar.Text = "Editar";
            this.btnEditar.Top = 260;
            this.btnEditar.Left = 120;
            this.btnEditar.Click += (s, e) => {
                if (dgvActores.CurrentRow == null) return;

                var actor = (ActorActriz)dgvActores.CurrentRow.DataBoundItem;
                var form = new EditarActorForm(actor);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _persistencia.ActorEdita(form.Codigo, form.Nombre, form.URLIMDB,
                        form.FechaNacimiento, form.Nacionalidad, form.Genero);
                    CargarActores();
                }
            };

            // btnBorrar
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.Top = 260;
            this.btnBorrar.Left = 220;
            this.btnBorrar.Click += (s, e) => {
                if (dgvActores.CurrentRow == null) return;

                var actor = (ActorActriz)dgvActores.CurrentRow.DataBoundItem;
                if (!_persistencia.ActorBorra(actor.Codigo))
                {
                    MessageBox.Show("No se puede borrar: El actor está en una serie");
                }
                CargarActores();
            };

            // btnSeries
            this.btnSeries.Text = "Ver Series";
            this.btnSeries.Top = 260;
            this.btnSeries.Left = 320;
            this.btnSeries.Click += (s, e) => {
                if (dgvActores.CurrentRow == null) return;

                var actor = (ActorActriz)dgvActores.CurrentRow.DataBoundItem;
                var series = _persistencia.ActorTrabaja(actor.Codigo);

                if (series.Count == 0)
                {
                    MessageBox.Show("Este actor no está en ninguna serie");
                    return;
                }

                MessageBox.Show($"Series de {actor.Nombre}:\n\n{string.Join("\n", series)}");
            };

            // ActoresForm
            this.Text = "Gestión de Actores";
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.AddRange(new Control[] { dgvActores, btnAgregar, btnEditar, btnBorrar, btnSeries });
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarActores()
        {
            dgvActores.DataSource = null;
            dgvActores.DataSource = _persistencia.Actores;
            dgvActores.AutoResizeColumns();
        }
    }

    public class EditarActorForm : Form
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string URLIMDB { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtURL;
        private DateTimePicker dtpFecha;
        private TextBox txtNacionalidad;
        private TextBox txtGenero;
        private Button btnAceptar;
        private Button btnCancelar;

        public EditarActorForm()
        {
            InitializeComponent();
            Text = "Agregar Actor";
        }

        public EditarActorForm(ActorActriz actor) : this()
        {
            Text = "Editar Actor";
            txtCodigo.Text = actor.Codigo.ToString();
            txtNombre.Text = actor.Nombre;
            txtURL.Text = actor.URLIMDB.Replace("https://www.imdb.com/name/", "");
            dtpFecha.Value = actor.FechaNacimiento;
            txtNacionalidad.Text = actor.Nacionalidad;
            txtGenero.Text = actor.Genero;
        }

        private void InitializeComponent()
        {
            this.txtCodigo = new TextBox();
            this.txtNombre = new TextBox();
            this.txtURL = new TextBox();
            this.dtpFecha = new DateTimePicker();
            this.txtNacionalidad = new TextBox();
            this.txtGenero = new TextBox();
            this.btnAceptar = new Button();
            this.btnCancelar = new Button();

            // Labels and controls
            var lblCodigo = new Label { Text = "Código:", Left = 20, Top = 20 };
            txtCodigo.Left = 120; txtCodigo.Top = 20; txtCodigo.Width = 200;

            var lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 50 };
            txtNombre.Left = 120; txtNombre.Top = 50; txtNombre.Width = 200;

            var lblURL = new Label { Text = "URL IMDB:", Left = 20, Top = 80 };
            txtURL.Left = 120; txtURL.Top = 80; txtURL.Width = 200;

            var lblFecha = new Label { Text = "Fecha Nac:", Left = 20, Top = 110 };
            dtpFecha.Left = 120; dtpFecha.Top = 110; dtpFecha.Width = 200;

            var lblNacionalidad = new Label { Text = "Nacionalidad:", Left = 20, Top = 140 };
            txtNacionalidad.Left = 120; txtNacionalidad.Top = 140; txtNacionalidad.Width = 200;

            var lblGenero = new Label { Text = "Género:", Left = 20, Top = 170 };
            txtGenero.Left = 120; txtGenero.Top = 170; txtGenero.Width = 200;

            // Buttons
            btnAceptar.Text = "Aceptar";
            btnAceptar.Left = 120; btnAceptar.Top = 210;
            btnAceptar.Click += (s, e) => {
                if (!ValidarDatos()) return;

                Codigo = int.Parse(txtCodigo.Text);
                Nombre = txtNombre.Text;
                URLIMDB = txtURL.Text;
                FechaNacimiento = dtpFecha.Value;
                Nacionalidad = txtNacionalidad.Text;
                Genero = txtGenero.Text;

                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Text = "Cancelar";
            btnCancelar.Left = 220; btnCancelar.Top = 210;
            btnCancelar.Click += (s, e) => {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            // Form
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.AddRange(new Control[] {
                lblCodigo, txtCodigo,
                lblNombre, txtNombre,
                lblURL, txtURL,
                lblFecha, dtpFecha,
                lblNacionalidad, txtNacionalidad,
                lblGenero, txtGenero,
                btnAceptar, btnCancelar
            });
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private bool ValidarDatos()
        {
            if (!int.TryParse(txtCodigo.Text, out _))
            {
                MessageBox.Show("Código inválido");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Nombre requerido");
                return false;
            }

            return true;
        }
    }

    public class SeriesForm : Form
    {
        private readonly Persistencia _persistencia;
        private DataGridView dgvSeries;
        private ListBox lstActores;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnBorrar;
        private Button btnAsociar;
        private Button btnDesvincular;

        public SeriesForm(Persistencia persistencia)
        {
            _persistencia = persistencia;
            InitializeComponent();
            CargarSeries();
        }

        private void InitializeComponent()
        {
            this.dgvSeries = new DataGridView();
            this.lstActores = new ListBox();
            this.btnAgregar = new Button();
            this.btnEditar = new Button();
            this.btnBorrar = new Button();
            this.btnAsociar = new Button();
            this.btnDesvincular = new Button();

            // dgvSeries
            this.dgvSeries.Dock = DockStyle.Top;
            this.dgvSeries.Height = 200;
            this.dgvSeries.ReadOnly = true;
            this.dgvSeries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSeries.SelectionChanged += (s, e) => {
                if (dgvSeries.CurrentRow == null) return;

                var serie = (Serie)dgvSeries.CurrentRow.DataBoundItem;
                lstActores.DataSource = _persistencia.SerieActores(serie.Codigo);
            };

            // lstActores
            this.lstActores.Top = 210;
            this.lstActores.Left = 20;
            this.lstActores.Width = 200;
            this.lstActores.Height = 100;

            // btnAgregar
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Top = 320;
            this.btnAgregar.Left = 20;
            this.btnAgregar.Click += (s, e) => {
                var form = new EditarSerieForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!_persistencia.SerieAdiciona(form.Codigo, form.Nombre, form.URLIMDB,
                        form.AnioEstreno, form.Genero, form.Temporadas))
                    {
                        MessageBox.Show("Error: El código ya existe");
                    }
                    CargarSeries();
                }
            };

            // btnEditar
            this.btnEditar.Text = "Editar";
            this.btnEditar.Top = 320;
            this.btnEditar.Left = 120;
            this.btnEditar.Click += (s, e) => {
                if (dgvSeries.CurrentRow == null) return;

                var serie = (Serie)dgvSeries.CurrentRow.DataBoundItem;
                var form = new EditarSerieForm(serie);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _persistencia.SerieEdita(form.Codigo, form.Nombre, form.URLIMDB,
                        form.AnioEstreno, form.Genero, form.Temporadas);
                    CargarSeries();
                }
            };

            // btnBorrar
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.Top = 320;
            this.btnBorrar.Left = 220;
            this.btnBorrar.Click += (s, e) => {
                if (dgvSeries.CurrentRow == null) return;

                var serie = (Serie)dgvSeries.CurrentRow.DataBoundItem;
                _persistencia.SerieBorra(serie.Codigo);
                CargarSeries();
            };

            // btnAsociar
            this.btnAsociar.Text = "Asociar Actor";
            this.btnAsociar.Top = 210;
            this.btnAsociar.Left = 240;
            this.btnAsociar.Click += (s, e) => {
                if (dgvSeries.CurrentRow == null) return;

                var serie = (Serie)dgvSeries.CurrentRow.DataBoundItem;
                var form = new SeleccionarActorForm(_persistencia);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!_persistencia.SerieAsocia(serie.Codigo, form.CodigoActorSeleccionado))
                    {
                        MessageBox.Show("Error: El actor ya está asociado");
                    }
                    CargarSeries();
                }
            };

            // btnDesvincular
            this.btnDesvincular.Text = "Desvincular";
            this.btnDesvincular.Top = 240;
            this.btnDesvincular.Left = 240;
            this.btnDesvincular.Click += (s, e) => {
                if (dgvSeries.CurrentRow == null || lstActores.SelectedItem == null) return;

                var serie = (Serie)dgvSeries.CurrentRow.DataBoundItem;
                var actorStr = lstActores.SelectedItem.ToString();

                // Extract actor code from [code] name format
                int start = actorStr.IndexOf('[') + 1;
                int end = actorStr.IndexOf(']');
                if (start < 0 || end <= start) return;

                string codigoStr = actorStr.Substring(start, end - start);
                if (!int.TryParse(codigoStr, out int codigoActor)) return;

                _persistencia.SerieDisocia(serie.Codigo, codigoActor);
                CargarSeries();
            };

            // SeriesForm
            this.Text = "Gestión de Series";
            this.ClientSize = new System.Drawing.Size(484, 371);
            this.Controls.AddRange(new Control[] {
                dgvSeries, lstActores,
                btnAgregar, btnEditar, btnBorrar,
                btnAsociar, btnDesvincular
            });
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarSeries()
        {
            dgvSeries.DataSource = null;
            dgvSeries.DataSource = _persistencia.Series;
            dgvSeries.AutoResizeColumns();
            lstActores.DataSource = null;
        }
    }

    public class EditarSerieForm : Form
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string URLIMDB { get; set; } = string.Empty;
        public int AnioEstreno { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int Temporadas { get; set; }

        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtURL;
        private NumericUpDown nudAnio;
        private TextBox txtGenero;
        private NumericUpDown nudTemporadas;
        private Button btnAceptar;
        private Button btnCancelar;

        public EditarSerieForm()
        {
            InitializeComponent();
            Text = "Agregar Serie";
            nudAnio.Value = DateTime.Now.Year;
        }

        public EditarSerieForm(Serie serie) : this()
        {
            Text = "Editar Serie";
            txtCodigo.Text = serie.Codigo.ToString();
            txtNombre.Text = serie.Nombre;
            txtURL.Text = serie.URLIMDB.Replace("https://www.imdb.com/title/", "");
            nudAnio.Value = serie.AnioEstreno;
            txtGenero.Text = serie.Genero;
            nudTemporadas.Value = serie.Temporadas;
        }

        private void InitializeComponent()
        {
            this.txtCodigo = new TextBox();
            this.txtNombre = new TextBox();
            this.txtURL = new TextBox();
            this.nudAnio = new NumericUpDown();
            this.txtGenero = new TextBox();
            this.nudTemporadas = new NumericUpDown();
            this.btnAceptar = new Button();
            this.btnCancelar = new Button();

            // Labels and controls
            var lblCodigo = new Label { Text = "Código:", Left = 20, Top = 20 };
            txtCodigo.Left = 120; txtCodigo.Top = 20; txtCodigo.Width = 200;

            var lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 50 };
            txtNombre.Left = 120; txtNombre.Top = 50; txtNombre.Width = 200;

            var lblURL = new Label { Text = "URL IMDB:", Left = 20, Top = 80 };
            txtURL.Left = 120; txtURL.Top = 80; txtURL.Width = 200;

            var lblAnio = new Label { Text = "Año Estreno:", Left = 20, Top = 110 };
            nudAnio.Left = 120; nudAnio.Top = 110; nudAnio.Width = 200;
            nudAnio.Minimum = 1900;
            nudAnio.Maximum = DateTime.Now.Year + 10;

            var lblGenero = new Label { Text = "Género:", Left = 20, Top = 140 };
            txtGenero.Left = 120; txtGenero.Top = 140; txtGenero.Width = 200;

            var lblTemporadas = new Label { Text = "Temporadas:", Left = 20, Top = 170 };
            nudTemporadas.Left = 120; nudTemporadas.Top = 170; nudTemporadas.Width = 200;
            nudTemporadas.Minimum = 1;
            nudTemporadas.Maximum = 100;

            // Buttons
            btnAceptar.Text = "Aceptar";
            btnAceptar.Left = 120; btnAceptar.Top = 210;
            btnAceptar.Click += (s, e) => {
                if (!ValidarDatos()) return;

                Codigo = int.Parse(txtCodigo.Text);
                Nombre = txtNombre.Text;
                URLIMDB = txtURL.Text;
                AnioEstreno = (int)nudAnio.Value;
                Genero = txtGenero.Text;
                Temporadas = (int)nudTemporadas.Value;

                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Text = "Cancelar";
            btnCancelar.Left = 220; btnCancelar.Top = 210;
            btnCancelar.Click += (s, e) => {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            // Form
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.AddRange(new Control[] {
                lblCodigo, txtCodigo,
                lblNombre, txtNombre,
                lblURL, txtURL,
                lblAnio, nudAnio,
                lblGenero, txtGenero,
                lblTemporadas, nudTemporadas,
                btnAceptar, btnCancelar
            });
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private bool ValidarDatos()
        {
            if (!int.TryParse(txtCodigo.Text, out _))
            {
                MessageBox.Show("Código inválido");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Nombre requerido");
                return false;
            }

            return true;
        }
    }

    public class SeleccionarActorForm : Form
    {
        private readonly Persistencia _persistencia;
        private DataGridView dgvActores;
        private Button btnSeleccionar;
        private Button btnCancelar;

        public int CodigoActorSeleccionado { get; set; }

        public SeleccionarActorForm(Persistencia persistencia)
        {
            _persistencia = persistencia;
            InitializeComponent();
            CargarActores();
        }

        private void InitializeComponent()
        {
            this.dgvActores = new DataGridView();
            this.btnSeleccionar = new Button();
            this.btnCancelar = new Button();

            // dgvActores
            this.dgvActores.Dock = DockStyle.Top;
            this.dgvActores.Height = 250;
            this.dgvActores.ReadOnly = true;
            this.dgvActores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // btnSeleccionar
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.Top = 260;
            this.btnSeleccionar.Left = 120;
            this.btnSeleccionar.Click += (s, e) => {
                if (dgvActores.CurrentRow == null) return;

                var actor = (ActorActriz)dgvActores.CurrentRow.DataBoundItem;
                CodigoActorSeleccionado = actor.Codigo;
                DialogResult = DialogResult.OK;
                Close();
            };

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Top = 260;
            this.btnCancelar.Left = 220;
            this.btnCancelar.Click += (s, e) => {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            // Form
            this.Text = "Seleccionar Actor";
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.AddRange(new Control[] { dgvActores, btnSeleccionar, btnCancelar });
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void CargarActores()
        {
            dgvActores.DataSource = _persistencia.Actores;
            dgvActores.AutoResizeColumns();
        }
    }

    // PROGRAM ENTRY POINT
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new Persistencia()));
        }
    }
}