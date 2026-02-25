using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_practico_PED
{
    public partial class Form1 : Form
    {
        Cola ColaAuxiliar;
        NodoCola nodosalida;
        Label lblNodo;
        int numeroNodo;
        int EjeX, EjeY;
        int largoNodo, distanciaEntreNodos, altoNodo;
        int posx, posy, X;
        int TipoMov = 1;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            NodoCola nuevo;
            string valor = txtNombre.Text;
            if (valor.Length > 0)
            {
                nuevo = new NodoCola(valor);
                ColaAuxiliar.Encolar(nuevo);
                ActivarControles(false);
                lblNodo = Etiqueta(valor, Color.LightGreen);
                gbxCola.Controls.Add(lblNodo);
                UbicarEtiqueta(lblNodo, ColaAuxiliar.TotalNodos());
            }
            else
            {
                txtNombre.Focus();
            }
        }

        public Form1()
        {
            InitializeComponent();
            dimencionarNodos();
            ColaAuxiliar = new Cola();
        }

        private void tmrActualizar_Tick(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Control etiqueta in gbxCola.Controls)
            {
                lblNodo = etiqueta as Label;
                lblNodo.Left -= 10;
                i++;
                if (i == 1 && lblNodo.Left <= distanciaEntreNodos)
                {
                    tmrActualizar.Enabled = false;
                    ActualizarNodos();
                }
            }
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            Control etiqueta;
            if (ColaAuxiliar.EstaVacia())
            {
                MessageBox.Show("Cola vacia", "Demo TAD Cola", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                nodosalida = ColaAuxiliar.Desencolar();
                etiqueta = gbxCola.Controls[0];
                lblNodo = etiqueta as Label;
                lblNodo.BackColor = Color.Yellow;
                ActivarControles(false);
                tmrEliminar.Start();
            }
        }

        private void tmrEliminar_Tick(object sender, EventArgs e)
        {
            lblNodo.Left -= 10;
            if (lblNodo.Left <= distanciaEntreNodos)
            {
                lblNodo.Top -= 10;
                if (lblNodo.Top < -1 * altoNodo)
                {
                    gbxCola.Controls.RemoveAt(0);
                    tmrEliminar.Stop();
                    if (ColaAuxiliar.TotalNodos() == 0) ActualizarNodos();
                    tmrActualizar.Start();
                }
            }
        }

        private void tmrAgregar_Tick(object sender, EventArgs e)
        {
            switch (TipoMov)
            {
                case 1:
                    if (posy <= EjeY)
                    {
                        posy += 10;
                    }
                    else
                    {
                        numeroNodo = ColaAuxiliar.TotalNodos();
                        X = numeroNodo * distanciaEntreNodos;
                        if (numeroNodo <= 6)
                        {
                            posx = 6 * distanciaEntreNodos;
                        }
                        else
                        {
                            posx = (numeroNodo + 1) * distanciaEntreNodos;
                        }
                        TipoMov = 2;
                    }
                    break;
                case 2:
                    if (posx > X)
                    {
                        posx -= 10;
                    }
                    else
                    {
                        tmrAgregar.Stop();
                        lblNodo.BackColor = Color.Orange;
                        ActualizarNodos();
                    }
                    break;

            }
            lblNodo.Left = posx;
            lblNodo.Top = posy;
        }

        public void dimencionarNodos()
        {
            distanciaEntreNodos = gbxCola.Width / 7;
            largoNodo = (int)(0.8 * distanciaEntreNodos);
            altoNodo = gbxCola.Height / 4;

            EjeX = distanciaEntreNodos / 2;
            EjeY = 2 * altoNodo;

            lblCliente.Size = new Size(largoNodo, altoNodo);
            lblCliente.AutoSize = false;
            lblCliente.Text = "";
            lblCliente.BackColor = Color.Yellow;
            lblCliente.Font = new Font(FontFamily.GenericSerif, 10f);
            lblCliente.TextAlign = ContentAlignment.MiddleCenter;
        }
        public Label Etiqueta(string valor, Color colorf)
        {
            Label nuevolabel = new Label();
            nuevolabel.Visible = false;
            nuevolabel.Size = new Size(largoNodo, altoNodo);
            nuevolabel.AutoSize = false;
            nuevolabel.Text = valor;
            nuevolabel.BackColor = colorf;
            nuevolabel.Font = new Font(FontFamily.GenericSerif, 10f);
            nuevolabel.TextAlign = ContentAlignment.MiddleCenter;
            return nuevolabel;
        }

        public void UbicarEtiqueta(Label nuevalabel, int idnodo)
        {
            posx = 6 * distanciaEntreNodos;
            posy = -altoNodo;
            X = idnodo * distanciaEntreNodos;
            nuevalabel.Width = largoNodo;
            nuevalabel.Left = posx;
            nuevalabel.Top = posy;
            nuevalabel.Visible = true;

            TipoMov = 1;
            tmrAgregar.Start();
        }

        public void ActivarControles(bool activar)
        {
            txtNombre.Enabled = activar;
            btnAgregar.Enabled = activar;
            btnAtender.Enabled = activar;
            btnMostrar.Enabled = activar;
            btnLimpiar.Enabled = activar;

            if (activar) txtNombre.Focus();
            else txtNombre.Text = "";
        }

        public void ActualizarNodos()
        {
            int totalNodos = ColaAuxiliar.TotalNodos();
            if (nodosalida != null)
            {
                lblCliente.Text = nodosalida.info;
                if (totalNodos == 0)
                    gbxCola.Text = "Cola esta vacia, sin nodos en su interior";
                else
                    gbxCola.Text = "Cola tiene " + totalNodos + " nodos en su interior";

                int nodosOcultos = ColaAuxiliar.TotalNodos() - 6;
                if (nodosOcultos > 1)
                    label3.Text = " y " + nodosOcultos.ToString() + "nodos mas";
                else
                    if (nodosOcultos == 1) label3.Text = " y un nodo mas";
                    else label3.Text = "";
            }

            ActivarControles(true);
        }
    }
}
