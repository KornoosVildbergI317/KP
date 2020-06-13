using ZooMail.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail
{
    class DBManager
    {
        private string connectionString = @"";
        public SqlDependency Dependency = new SqlDependency();
        public DataTable dtSpecies = new DataTable("Species");

        SqlConnection connection;

        public bool IsConnection { get; set; }
        public DBManager()
        {
            Configuration_class configuration_Class = new Configuration_class();
            configuration_Class.SQL_Server_Configuration_Get();
            if (!configuration_Class.isConnection)
            {
                IsConnection = false;
                return;
            }
            connection = configuration_Class.connection;
            IsConnection = true;
            try
            {
                connection.Open();

            }
            catch
            {
                IsConnection = false;
            }
        }


        public SqlDataReader execProc(string procName, Dictionary<string, Object> args)
        {


            SqlCommand command = new SqlCommand(procName, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (args != null)
            {
                foreach (var it in args)
                {
                    SqlParameter nameParam;
                    nameParam = new SqlParameter
                    {
                        ParameterName = it.Key,
                        Value = it.Value
                    };
                    command.Parameters.Add(nameParam);
                }
            }

            SqlDataReader reader = command.ExecuteReader();

            return reader;

        }



        public List<ModelCategory> getCategoryList()
        {
            List<ModelCategory> modelCategories = new List<ModelCategory>();

            var reader = execProc("Category_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelCategories.Add(new ModelCategory(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            reader.Close();
            return modelCategories;
        }

        public void insertCategory(Models.ModelCategory model)
        {
            execProc("Category_insert", new Dictionary<string, object> {
                { "Title", model.Title }
            });
        }

        public void deleteCategory(int ID_category)
        {
            execProc("Category_delete", new Dictionary<string, object> {
                { "ID_category", ID_category}

            });
        }

        public void updateCategory(Models.ModelCategory model)
        {
            execProc("Category_update", new Dictionary<string, object> {
                {"ID_category", model.Id_category } ,
                { "Title", model.Title }
            });
        }

        public List<ModelStatusClient> getStatusClientList()
        {
            List<ModelStatusClient> modelStatusClient = new List<ModelStatusClient>();

            var reader = execProc("status_client_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelStatusClient.Add(new ModelStatusClient(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            reader.Close();
            return modelStatusClient;
        }

        public void insertStatusClient(Models.ModelStatusClient model)
        {
            execProc("status_client_insert", new Dictionary<string, object> {
                { "Title", model.Title }
            });
        }

        public void deleteStatusClient(int ID_status_client)
        {
            execProc("status_client_delete", new Dictionary<string, object> {
                { "ID_status_client", ID_status_client}

            });
        }

        public void updateStatusClient(Models.ModelStatusClient model)
        {
            execProc("status_client_update", new Dictionary<string, object> {
                {"ID_status_client", model.Id_status_client } ,
                { "Title", model.Title }
            });
        }

        public List<ModelPosition> getPositionList()
        {
            List<ModelPosition> modelPosition = new List<ModelPosition>();

            var reader = execProc("Position_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelPosition.Add(new ModelPosition(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            reader.Close();
            return modelPosition;
        }

        public void insertPosition(Models.ModelPosition model)
        {
            execProc("Position_insert", new Dictionary<string, object> {
                { "Title_position", model.Title_position}
            });
        }

        public void deletePosition(int ID_Position)
        {
            execProc("Position_delete", new Dictionary<string, object> {
                { "ID_Position ", ID_Position }

            });
        }

        public void updatePosition(Models.ModelPosition model)
        {
            execProc("Position_update", new Dictionary<string, object> {
                {"ID_Position ", model.Id_Position } ,
                { "Title_position", model.Title_position }
            });
        }

        public List<ModelStatusAnimal> getStatusAnimalList()
        {
            List<ModelStatusAnimal> modelStatusAnimal = new List<ModelStatusAnimal>();

            var reader = execProc("Status_Animal_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelStatusAnimal.Add(new ModelStatusAnimal(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            reader.Close();
            return modelStatusAnimal;
        }

        public void insertStatusAnimal(Models.ModelStatusAnimal model)
        {
            execProc("Status_Animal_insert", new Dictionary<string, object> {
                { "Title", model.Title }
            });
        }

        public void deleteStatusAnimal(int ID_Status_Animal)
        {
            execProc("Status_Animal_delete", new Dictionary<string, object> {
                { "ID_Status_Animal ", ID_Status_Animal }

            });
        }

        public void updateStatusAnimal(Models.ModelStatusAnimal model)
        {
            execProc("Status_Animal_update", new Dictionary<string, object> {
                {"ID_Status_Animal ", model.Id_Status_Animal } ,
                { "Title", model.Title }
            });
        }

        public List<ModelSpecies> getSpeciesList()
        {
            List<ModelSpecies> modelSpecies = new List<ModelSpecies>();

            var reader = execProc("Species_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelSpecies.Add(new ModelSpecies(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            reader.Close();
            return modelSpecies;
        }

        public void insertSpecies(Models.ModelSpecies model)
        {
            execProc("Species_insert", new Dictionary<string, object> {
                { "Title", model.Title }
            });
        }

        public void deleteSpecies(int ID_species)
        {
            execProc("Species_delete", new Dictionary<string, object> {
                { "ID_species ", ID_species }

            });
        }

        public void updateSpecies(Models.ModelSpecies model)
        {
            execProc("Species_update", new Dictionary<string, object> {
                {"ID_species ", model.Id_species } ,
                { "Title", model.Title }
            });
        }

 
        public List<ModelRole> getRoleList(int idRole = -1)
        {
            List<ModelRole> modelRole = new List<ModelRole>();

            Dictionary<string, Object> param = null;

            if (idRole >= 0)
            {
                param = new Dictionary<string, Object> { { "ID_ROLE", idRole } };
            }
            else
            {
                param = new Dictionary<string, Object> { { "ID_ROLE", "" } };
            }

            var reader = execProc("Role_select", param);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelRole.Add(new ModelRole(reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetInt32(6)));
                }
            }
            reader.Close();
            return modelRole;
        }

        public void insertRole(Models.ModelRole model)
        {
            execProc("Role_insert", new Dictionary<string, object> {
                {"Title_Role ",model.Title_Role },
                {"Add_client ",model.Add_client },
                {"Add_animal ",model.Add_animal },
                {"Buy_animal ",model.Buy_animal },
                {"Lock_client ",model.Lock_client },
                {"Accounting_animals ",model.Accounting_animals }
            });

        }

        public void deleteRole(int ID_Role)
        {
            execProc("Role_delete", new Dictionary<string, object> {
                { "ID_Role ", ID_Role }

            });
        }

        public void updateRole(Models.ModelRole model)
        {
            execProc("Role_update", new Dictionary<string, object> {
                { "ID_Role ", model.Id_Role } ,
                {"Title_Role ",model.Title_Role },
                {"Add_client ",model.Add_client },
                {"Add_animal ",model.Add_animal },
                {"Buy_animal ",model.Buy_animal },
                {"Lock_client ",model.Lock_client },
                {"Accounting_animals ",model.Accounting_animals }
            });

        }


        public List<ModelAnimalparkDetail> getAnimalparkListDetail()
        {
            List<ModelAnimalparkDetail> modelAnimalparkdetail = new List<ModelAnimalparkDetail>();

            var reader = execProc("Animalpark_select_detail", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelAnimalparkdetail.Add(new ModelAnimalparkDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
            }
            reader.Close();
            return modelAnimalparkdetail;
        }

        public List<ModelAnimalpark> getAnimalparkList()
        {
            List<ModelAnimalpark> modelAnimalpark = new List<ModelAnimalpark>();

            var reader = execProc("Animalpark_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelAnimalpark.Add(new ModelAnimalpark(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                }
            }
            reader.Close();
            return modelAnimalpark;
        }

        public void insertAnimalpark(Models.ModelAnimalpark model)
        {
            execProc("Animalpark_insert", new Dictionary<string, object> {
                { "Number", model.Number },
                {"ID_Category ",model.ID_Category },
                {"ID_Supplier ",model.ID_Supplier },
                {"ID_Status_Animal ",model.ID_Status_Animal }
            });
        }

        public void deleteAnimalpark(int ID_animalpark)
        {
            execProc("Animalpark_delete", new Dictionary<string, object> {
                { "ID_animalpark ", ID_animalpark }

            });
        }

        public void updateAnimalpark(Models.ModelAnimalpark model)
        {
            execProc("Animalpark_update", new Dictionary<string, object> {
                { "ID_animalpark ", model.ID_animalpark } ,
                { "Number", model.Number },
                {"ID_Category ",model.ID_Category },
                {"ID_Supplier ",model.ID_Supplier },
                {"ID_Status_Animal ",model.ID_Status_Animal }
            });
        }

        public List<ModelSupplierDetail> getSupplierListDetail()
        {
            List<ModelSupplierDetail> modelSupplierdetail = new List<ModelSupplierDetail>();

            var reader = execProc("Model_select_detail", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelSupplierdetail.Add(new ModelSupplierDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }
            reader.Close();
            return modelSupplierdetail;
        }

        public List<ModelSupplier> getSupplierList()
        {
            List<ModelSupplier> modelSupplier = new List<ModelSupplier>();

            var reader = execProc("Supplier_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelSupplier.Add(new ModelSupplier(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                }
            }
            reader.Close();
            return modelSupplier;
        }

        public void insertSupplier(Models.ModelSupplier model)
        {
            execProc("Supplier_insert", new Dictionary<string, object> {
                { "Title", model.Title },
                {"Release_date ",model.Release_date },
                {"ID_Species ",model.Id_Species }
            });
        }

        public void deleteSupplier(int ID_supplier)
        {
            execProc("Supplier_delete", new Dictionary<string, object> {
                { "ID_supplier ", ID_supplier }

            });
        }

        public void updateSupplier(Models.ModelSupplier model)
        {
            execProc("Supplier_update", new Dictionary<string, object> {
                { "ID_supplier", model.Id_Supplier },
                { "Title", model.Title },
                {"Release_date ",model.Release_date },
                {"ID_Species ",model.Id_Species }
            });
        }

        public List<ModelBuy_animal> getBuy_animalList()
        {
            List<ModelBuy_animal> modelBuy_animal = new List<ModelBuy_animal>();

            var reader = execProc("Buy_animal_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelBuy_animal.Add(new ModelBuy_animal(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                }
            }
            reader.Close();
            return modelBuy_animal;
        }

        public List<ModelBuy_animalDetail> getBuy_animalListDetail()
        {
            List<ModelBuy_animalDetail> modelBuy_animalDetail = new List<ModelBuy_animalDetail>();

            var reader = execProc("Buy_animal_select_detail", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelBuy_animalDetail.Add(new ModelBuy_animalDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
            }
            reader.Close();
            return modelBuy_animalDetail;
        }

        public void insertBuy_animal(Models.ModelBuy_animal model)
        {
            execProc("Buy_animal_insert", new Dictionary<string, object> {
                { "Buy_animal_date_and_time", model.Buy_animal_date_and_time },
                {"Import_date_and_time ",model.Import_date_and_time },
                {"ID_animalpark ",model.ID_animalpark },
                {"ID_Authorization",model.ID_Authorization }
            });
        }

        public void deleteBuy_animal(int ID_Buy_animal)
        {
            execProc("Buy_animal_delete", new Dictionary<string, object> {
                { "ID_Buy_animal ", ID_Buy_animal }

            });
        }

        public void updateBuy_animal(Models.ModelBuy_animal model)
        {
            execProc("Buy_animal_update", new Dictionary<string, object> {
                { "ID_Buy_animal", model.ID_Buy_animal },
                { "Buy_animal_date_and_time", model.Buy_animal_date_and_time },
                {"Import_date_and_time ",model.Import_date_and_time },
                {"ID_Animalpark ",model.ID_animalpark },
                {"ID_Authorization",model.ID_Authorization }
            });
        }

        public List<ModelEmployee> getEmployeeList()
        {
            List<ModelEmployee> modelEmployee = new List<ModelEmployee>();

            var reader = execProc("Employee_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelEmployee.Add(new ModelEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
            }
            reader.Close();
            return modelEmployee;
        }

        public void insertEmployee(Models.ModelEmployee model)
        {
            execProc("Employee_insert", new Dictionary<string, object> {
                { "ID_Authorization",model.ID_Authorization},
                { "Surname", model.Surname },
                {"Name", model.Name },
                { "Middle_name", model.Middle_Name }
            });
        }

        public void deleteEmployee(int ID_Authorization)
        {
            execProc("Employee_delete", new Dictionary<string, object> {
                { "ID_Authorization ", ID_Authorization }

            });
        }

        public void updateEmployee(Models.ModelEmployee model)
        {
            execProc("Employee_update", new Dictionary<string, object> {
                { "ID_Authorization",model.ID_Authorization},
                { "Surname", model.Surname },
                {"Name", model.Name },
                { "Middle_name", model.Middle_Name }
            });
        }

        public List<ModelAuthorization> getAuthorizationList()
        {
            List<ModelAuthorization> modelAuthorization = new List<ModelAuthorization>();

            var reader = execProc("authorization_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelAuthorization.Add(new ModelAuthorization(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetInt32(3)));
                }
            }
            reader.Close();
            return modelAuthorization;
        }

        public void insertAuthorization(Models.ModelAuthorization model)
        {
            execProc("Authorization_insert", new Dictionary<string, object> {
                { "Login", model.Login },
                { "Password", model.Password },
                { "ID_Role", model.ID_Role }
            });
        }

        public void deleteAuthorization(int ID_Authorization)
        {
            execProc("Authorization_delete", new Dictionary<string, object> {
                { "ID_Authorization ", ID_Authorization }

            });
        }

        public void updateAuthorization(Models.ModelAuthorization model)
        {
            execProc("Authorization_update", new Dictionary<string, object> {
                { "ID_Authorization",model.ID_Authorization},
                { "Login", model.Login },
                { "Password", model.Password },
                { "ID_Role", model.ID_Role }
            });
        }

        public List<ModelClient> getClientList()
        {
            List<ModelClient> modelClient = new List<ModelClient>();

            var reader = execProc("Client_select", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelClient.Add(new ModelClient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(7)));
                }
            }
            reader.Close();
            return modelClient;
        }

        public List<ModelClientDetail> getClientListDetail()
        {
            List<ModelClientDetail> modelClient = new List<ModelClientDetail>();

            var reader = execProc("Client_select_detail", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modelClient.Add(new ModelClientDetail(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }
            reader.Close();
            return modelClient;
        }

        public void deleteClient(int ID_Authorization)
        {
            execProc("Client_delete", new Dictionary<string, object> {
                { "ID_Authorization", ID_Authorization}

            });
        }

        public void updateClient(Models.ModelClient model)
        {
            execProc("Client_update", new Dictionary<string, object> {
                { "ID_Authorization", model.ID_Authorization} ,
                {"Surname ",model.Surname },
                {"Name ",model.Name },
                {"Middle_Name ",model.Middle_Name },
                {"Email_address ",model.Email_address },
                {"ID_status_client",model.ID_status_client },
                {"Passport_number_and_series ",model.Passport_number_and_series },
                {"ID_Category",model.ID_Category }
            });

        }

        public void insertClient(Models.ModelClient model)
        {
            execProc("Client_insert", new Dictionary<string, object> {
                { "ID_Authorization", model.ID_Authorization} ,
                {"Surname ",model.Surname },
                {"Name ",model.Name },
                {"Middle_Name ",model.Middle_Name },
                {"Email_address ",model.Email_address },
                {"ID_status_client",model.ID_status_client },
                {"Passport_number_and_series ",model.Passport_number_and_series },
                {"ID_Category",model.ID_Category }
            });

        }

    }
}
