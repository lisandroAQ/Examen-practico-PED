using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_practico_PED
{
    public partial class Form1 : Form
    {
        ColaEnlazada ColaAuxiliar;

        public Form1()
        {
            InitializeComponent();
            ColaAuxiliar = new ColaEnlazada();
            EstiloModerno();
        }

        // ── Botón Agregar ────────────────────────────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string valor = txtNombre.Text.Trim();
            if (valor.Length == 0)
            {
                MessageBox.Show("Ingrese un nombre.", "Farmacia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            ColaAuxiliar.Encolar(new NodoCola(valor));
            ActualizarContador();
            txtNombre.Clear();
            txtNombre.Focus();
            panelCola.Invalidate(); // Redibujar
        }

        // ── Botón Atender siguiente ──────────────────────────────────────
        private void btnAtender_Click(object sender, EventArgs e)
        {
            if (ColaAuxiliar.EstaVacia())
            {
                lblCliente.Text = "—";
                return;
            }

            NodoCola atendido = ColaAuxiliar.Desencolar();
            lblCliente.Text = atendido.info;

            panelCola.Invalidate();
        }

        // ── Botón Mostrar lista de espera ────────────────────────────────
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (ColaAuxiliar.EstaVacia())
            {
                MessageBox.Show("La cola está vacía.", "Lista de espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string lista = ColaAuxiliar.MostrarTodos();
            MessageBox.Show(lista, "Lista de espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarContador()
        {
            label3.Text = "Clientes en cola: " + ColaAuxiliar.TotalNodos();
        }


        // ── Paint del Panel (Drawing) ────────────────────────────────────
        private void panelCola_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            if (ColaAuxiliar.EstaVacia())
            {
                g.DrawString("Cola vacía", new Font("Arial", 12, FontStyle.Italic),
                             Brushes.Gray, new PointF(10, 10));
                return;
            }

            int nodoAncho = 110;
            int nodoAlto = 50;
            int separacion = 40;   // espacio entre nodos (donde va la flecha)
            int startX = 20;
            int startY = (panelCola.Height - nodoAlto) / 2;

            Pen penNodo = new Pen(Color.DarkBlue, 2);
            Pen penFlecha = new Pen(Color.FromArgb(60, 60, 60), 3);
            penFlecha.StartCap = LineCap.Round;

            // Flecha personalizada (más visible)
            AdjustableArrowCap arrowCap = new AdjustableArrowCap(6, 6, true);
            penFlecha.CustomEndCap = arrowCap;
            Font fuenteTexto = new Font("Arial", 9, FontStyle.Bold);
            SolidBrush brushFondo = new SolidBrush(Color.LightSkyBlue);
            SolidBrush brushTexto = new SolidBrush(Color.Black);
            SolidBrush brushPrimero = new SolidBrush(Color.LightGreen);

            NodoCola actual = ColaAuxiliar.Primero();
            int x = startX;
            bool esPrimero = true;

            while (actual != null)
            {

                Rectangle rect = new Rectangle(x, startY, nodoAncho, nodoAlto);

                // Fondo: verde para el primero (próximo a atender), azul para los demás
                g.FillRectangle(esPrimero ? brushPrimero : brushFondo, rect);
                g.DrawRectangle(penNodo, rect);

                // Nombre del cliente centrado
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(actual.info, fuenteTexto, brushTexto, rect, sf);

                // Etiqueta "PRIMERO" debajo del primer nodo
                if (esPrimero)
                {
                    g.DrawString("▶ PRIMERO", new Font("Arial", 7), Brushes.DarkGreen,
                                 new PointF(x, startY + nodoAlto + 2));
                }

                // Flecha hacia el siguiente nodo
                if (actual.sig != null)
                {
                    int flechaX1 = x + nodoAncho;
                    int flechaX2 = x + nodoAncho + separacion;
                    int flechaY = startY + nodoAlto / 2;
                    g.DrawLine(penFlecha, flechaX1, flechaY, flechaX2, flechaY);
                }

                x += nodoAncho + separacion;
                actual = actual.sig;
                esPrimero = false;
            }

            penNodo.Dispose();
            penFlecha.Dispose();
            fuenteTexto.Dispose();
            brushFondo.Dispose();
            brushTexto.Dispose();
            brushPrimero.Dispose();
        }
        private void EstiloModerno()
        {
            // Fondo general
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Panel de la cola
            panelCola.BackColor = Color.White;
            panelCola.BorderStyle = BorderStyle.None;

            // GroupBox estilo
            groupBox1.ForeColor = Color.FromArgb(45, 62, 80);
            groupBox2.ForeColor = Color.FromArgb(45, 62, 80);

            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            // Labels
            label1.Font = new Font("Segoe UI", 10F);
            label2.Font = new Font("Segoe UI", 10F);
            lblCliente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCliente.ForeColor = Color.FromArgb(0, 120, 215);

            // TextBox moderno
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;

            // Botones estilo moderno
            EstiloBoton(btnAgregar, Color.FromArgb(0, 120, 215));
            EstiloBoton(btnAtender, Color.FromArgb(40, 167, 69));
            EstiloBoton(btnMostrar, Color.FromArgb(255, 193, 7));
            EstiloBoton(btnLimpiar, Color.FromArgb(220, 53, 69));
        }
        private void EstiloBoton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (ColaAuxiliar.EstaVacia())
                return;

            ColaAuxiliar.Limpiar();

            lblCliente.Text = "—";
            ActualizarContador();

            panelCola.Invalidate();
        }

       
    }
}

