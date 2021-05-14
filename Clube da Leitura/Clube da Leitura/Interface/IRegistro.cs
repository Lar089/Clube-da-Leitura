using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Interface
{
    interface IRegistro
    {
        void FinalizarRegistro();

        void VisualizarRegistrosFechados();

        void VisualizarRegistrosAbertos();
    }
}
