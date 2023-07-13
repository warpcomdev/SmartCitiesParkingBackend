namespace SCParking.Domain.Entities
{
    public class LeadModel
    {
        public string id { get; set; }
        public string laiaToken { get; set; }
        public int cliente { get; set; }
        public int proveedor { get; set; }
        public int categoriaOrigen { get; set; }
        public string servicio { get; set; }
        public string centro { get; set; }
        public string telefono { get; set; }
        public string producto { get; set; }
        public string categoriaLead1 { get; set; }
        public string categoriaLead2 { get; set; }
        public string nombreCompleto { get; set; }
        public string email { get; set; }
        public string familiaCliente { get; set; }
        public string fecha { get; set; }
        public object informacionExtra { get; set; }
        public string idLanding { get; set; }
        public string idLead { get; set; }
    }
}
