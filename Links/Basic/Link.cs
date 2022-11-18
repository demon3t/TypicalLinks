using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Links.Basic
{
    public abstract class Link

    {
        /// <summary>
        /// Время
        /// </summary>
        public static double i { get; set; }

        /// <summary>
        /// Аналитическое выражение.
        /// </summary>
        public abstract double h { get; set; }

        /// <summary>
        /// Коэффициент передачи (коэффициент усиления), отношение выходной величины к входной в установившемся режиме.
        /// </summary>
        public double K { get; set; }

        /// <summary>
        /// Постоянная времени.
        /// </summary>
        public double T { get; set; }

        /// <summary>
        /// Узел складывается в конечном узле. false - только для расширенного узла.
        /// </summary>
        public bool IsAdding { get; set; } = false;

        /// <summary>
        /// Узел начала.
        /// </summary>
        public Vertex Start { get; set; }

        /// <summary>
        /// Узел конца.
        /// </summary>
        public Vertex End { get; set; }

        /// <summary>
        /// Уникальный идентификатор звена.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();
    }
}
