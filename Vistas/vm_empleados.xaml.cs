using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EstandarXs.Interfaces;

namespace ControlUniformes.Vistas
{
    /// <summary>
    /// Interaction logic for vm_empleados.xaml
    /// </summary>
    public partial class vm_empleados : UserControl, ItoolPadre
    {
        public vm_empleados()
        {
            InitializeComponent();
        }


        #region Variables
        private bool _Inicializated = false;

        #endregion Variables

        #region metodos
        private void Inicializar()
        {
            if (_Inicializated == true) return;

            _Inicializated = true;
            ucGridPadre1.Ensamblado = "ControlUniformes.exe";
            ucGridPadre1.Entidad = "empleado";
            ucGridPadre1.FormaDetalle = "ControlUniformes.Formas.frm_empleados";
            ucGridPadre1.IconoVentanaDetalle = ((EstandarXs.Clases.ClosableTab)(this.Parent)).IconoVentanaDetalle;
            ucGridPadre1.AddCol(EstandarXs.Properties.Resources.gen_clave, "clave");
            ucGridPadre1.AddCol(EstandarXs.Properties.Resources.gen_nombre, "(LTRIM(RTRIM(isnull(appaterno,'') +' ' + isnull(apmaterno,'') + ' ' + nombre))) AS nombrecompleto");
           
        }

        #endregion metodos

        #region ItoolPadre Members

        public void Nuevo()
        {
            ucGridPadre1.Nuevo();
        }

        public void Editar()
        {
            ucGridPadre1.Editar();
        }

        public void Eliminar()
        {
            ucGridPadre1.Eliminar();
        }

        public void Refrescar()
        {
            this.Cursor = Cursors.Wait;
            ucGridPadre1.Refrescar();
            this.Cursor = Cursors.Arrow;
        }

        public void Importar()
        { }

        public void Exportar()
        { }
        public void Cerrar()
        { }

        #endregion

        #region Evnetos Forma

        private void ucGridPadre1_Loaded(object sender, RoutedEventArgs e)
        {

            Inicializar();
            _Inicializated = true;
        }

        #endregion Evnetos Forma
    }
}
