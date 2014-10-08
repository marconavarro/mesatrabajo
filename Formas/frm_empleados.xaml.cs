using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataModel;
using EstandarXs.Clases;
using EstandarXs.Controles;
using EstandarXs.Interfaces;

namespace ControlUniformes.Formas
{
    /// <summary>
    /// Interaction logic for frm_empleados.xaml
    /// </summary>
    public partial class frm_empleados : Window , ItoolDetalle
    {
        public frm_empleados()
        {
            InitializeComponent();
        }


        #region Varibles

        private empleado entidad;
        private Entities dataContext = new Entities(Sesion.CurrentDataContext.Connection.ConnectionString);

        #endregion Varibles

        #region Metodos

        /// <summary>
        /// Incializa la ventana
        /// </summary>
        private void Inicializar()
        {

            #region Max Length


            txtClave.MaxLength = (int)Util.GetMaxLength(entidad, "clave");
            txtNombre.MaxLength = (int)Util.GetMaxLength(entidad, "nombre");
            txtMaterno.MaxLength = (int)Util.GetMaxLength(entidad, "apmaterno");
            txtPaterno.MaxLength = (int)Util.GetMaxLength(entidad, "appaterno");
           
            #endregion Max Length

            #region Bind
            if (cboEstatus.Items.Count == 0)
                Enumeradores.CargarCombo(cboEstatus, typeof(Enumeradores.eEstatusGeneral), false);

            Util.BindControl(txtClave, TextBox.TextProperty, entidad, "clave", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            Util.BindControl(txtMaterno, TextBox.TextProperty, entidad, "apmaterno", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            Util.BindControl(txtNombre, TextBox.TextProperty, entidad, "nombre", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            Util.BindControl(txtPaterno, TextBox.TextProperty, entidad, "appaterno", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
           
            //pendiente de resolver
           
            Util.BindControl(cboEstatus, ComboBox.TextProperty, entidad, "estatus", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);

            if (string.IsNullOrEmpty(entidad.estatus) != true)
                cboEstatus.SelectedIndex = Enumeradores.getValorEnumeracion(typeof(Enumeradores.eEstatusGeneral), entidad.estatus);


            entidad.HayCambio = false;

            #endregion Bind
        }

        #endregion Metodos

        #region ItoolDetalle Members

        public bool Guardar()
        {
            
            if (entidad.HayError == true)
            {
                MessageBox.Show(EstandarXs.Properties.Resources.gen_error_validacion, EstandarXs.Properties.Resources.gen_aviso, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            dataContext.SaveChanges();

            entidad.HayCambio = false;

            MessageBox.Show(EstandarXs.Properties.Resources.gen_record_saved.ToString(), EstandarXs.Properties.Resources.gen_DXMessageBoxtitle.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);

            return true;

        }

        public void GuardaryNuevo()
        {

            if (Guardar() == false) return;

            dataContext = new Entities(Sesion.CurrentDataContext.Connection.ConnectionString);
            entidad = new empleado();
            dataContext.empleado.AddObject(entidad);
            Inicializar();
            txtClave.IsEnabled = true;
            txtClave.Focus();
        }

        public void GuardaryCerrar()
        {
            if (Guardar() == false) return;

           
            this.Close();
        }

        public void Salir()
        {

            this.Close();
        }

        #endregion

        #region Eventos Forma

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string reference = this.Tag as string;

            if (string.IsNullOrEmpty(reference) == true)
            {
                entidad = new empleado();
                dataContext.empleado.AddObject(this.entidad);
                txtClave.IsEnabled = true;
                txtClave.Focus();
            }
            else
            {

                entidad = empleado.Buscar(reference, dataContext);
                txtClave.IsEnabled = false;
                txtNombre.Focus();
            }
            Inicializar();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (entidad.HayCambio == true)
            {
                if (MessageBox.Show(string.Format(EstandarXs.Properties.Resources.lang_val_cambios_pendientes, Environment.NewLine),EstandarXs.Properties.Resources.gen_aviso, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            dataContext.Dispose();
        }

        #endregion Eventos Forma


    }
}
