using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class auditoriaDataMapper
    {
        public static auditoriaDataModel ToModel(this auditoria entity) => new()
        {
            Id = entity.id,
            Accion = entity.accion,
            TablaAfectada = entity.tabla_afectada,
            RegistroId = entity.registro_id ?? Guid.Empty, // Fix CS0266 y CS8629
            UsuarioId = entity.usuario_id ?? Guid.Empty,   // Fix CS0266 y CS8629
            FechaHora = entity.fecha_hora ?? DateTime.MinValue, // Fix CS0266 y CS8629
            DatosAnteriores = entity.datos_anteriores
        };

        public static auditoria ToEntity(this auditoriaDataModel model) => new()
        {
            accion = model.Accion,
            tabla_afectada = model.TablaAfectada,
            registro_id = model.RegistroId, // Ya no necesita '??', es compatible
            usuario_id = model.UsuarioId,   // Ya no necesita '??', es compatible
            datos_anteriores = model.DatosAnteriores,
            fecha_hora = model.FechaHora    // Ya no necesita '??', es compatible
        };
    }
}
