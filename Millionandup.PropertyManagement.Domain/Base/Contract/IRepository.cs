﻿using Microsoft.EntityFrameworkCore;

namespace Millionandup.PropertyManagement.Domain.Base.Contract
{
    /// <summary>
    /// Define los atributos y comportamientos de un repositorio
    /// </summary>
    public interface IRepository : IDisposable
    {
        #region Properties

        /// <summary>
        /// Obtiene el contexto de datos
        /// </summary>
        DbContext Context { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Inicializa el repositorio con el contexto
        /// </summary>
        /// <param name="context">Contexto de datos al que pertenece el repositorio</param>
        void Initialize(DbContext context);

        #endregion
    }
}
