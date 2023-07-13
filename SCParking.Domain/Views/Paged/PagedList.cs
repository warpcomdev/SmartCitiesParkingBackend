using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SCParking.Domain.Views.Paged
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// Indentifica que el es posible ir  hacia atras
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Indentifica que el es posible ir  hacia delante
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;


        public int? NextPageNumber => HasNextPage ? CurrentPage +1 : (int?)null;

        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;


        public PagedList(List<T> items , int count ,int  pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling( TotalCount / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, Dictionary<string, string> page = null, Dictionary<string, string> order = null)
        {

            //var ddd  = SortPaged(source, order);
            PagedList<T> data = new PagedList<T>((List<T>)source, 0, 0, 0);
            data = order != null ? SortPaged(data, order) : data;
            var pageNumber =  1;
            var pagesSize = 10;
            if(page!=null)
            {
                if (page.ContainsKey("number"))
                    pageNumber = int.Parse(page["number"]);

                if (page.ContainsKey("size"))
                    pagesSize = int.Parse(page["size"]);

            }

            var count = data.Count();
            if(page!=null)
            {
                var items = data.Skip((pageNumber - 1) * pagesSize).Take(pagesSize).ToList();
                PagedList<T> response = new PagedList<T>(items, count, pageNumber, pagesSize);
                //if (order != null)
                //{
                //    data = SortPaged(data, order);
                //}

                return response;
            }
            else
            {
                var items = data.ToList();
                PagedList<T> response = new PagedList<T>(items, count, pageNumber, pagesSize);

                return response;
            }
           
        }



        public static PagedList<T> SortPaged(PagedList<T> data, Dictionary<string, string> order)
        {
           
            // Ordenamiento
            if (order != null)
            {
                var orderList = new List<T>();
                string orderAscending = string.Empty;
                string orderDecending = string.Empty;
                //PropertyInfo[] lst = typeof(order.GetType()).GetProperties();
                //PropertyInfo[] lst = order.GetType().GetProperties();

                foreach (KeyValuePair<string, string> itemOrden in order)
                {
                    var keyField = itemOrden.Key;

                    if (itemOrden.Key == "date")
                        keyField = "fullDate";

                    if (itemOrden.Value == "asc")
                    {
                        orderAscending = $"{orderAscending},{keyField}";
                    }
                    if (itemOrden.Value == "desc")
                    {
                        orderDecending = $"{orderDecending},{keyField}";
                    }
                }

                /*foreach (PropertyInfo oProperty in lst)
                {
                    string nameProperty = oProperty.Name;
                    var value = oProperty.GetValue(order)?.ToString();
                    //var ordenado = new List<Customer>();
                    if (value == "asc")
                    {
                        orderAscending = $"{orderAscending},{nameProperty}";
                    }
                    if (value == "desc")
                    {
                        orderDecending = $"{orderDecending},{nameProperty}";
                    }

                }*/

                //Adiciono datos originales por si entra un order de una propiedad que no existe
                //, se agrega try - catch para controlar propiedad que no exista y evitar error 500
                orderList = data.ToList();

                if (!string.IsNullOrEmpty(orderAscending))
                {
                    orderAscending = orderAscending.Substring(1);
                    try
                    {
                        orderList = data.OrderBy($"{orderAscending} ascending").ToList();
                    }
                    catch (Exception ex)
                    {

                    }
                   
                }
                if (!string.IsNullOrEmpty(orderDecending))
                {
                    orderDecending = orderDecending.Substring(1);    
                    try
                    {
                        orderList = data.OrderBy($"{orderDecending} descending").ToList();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //Temporal mientras se encuentra otra forma de evitar error de tipo de dato
                data.Clear();
                foreach (var orderData in orderList)
                {
                    data.Add(orderData);
                }

            }

            return data;
        }


    }
}
