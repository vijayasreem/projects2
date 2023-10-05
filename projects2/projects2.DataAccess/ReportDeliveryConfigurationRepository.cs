using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace projects2
{
    public class ReportDeliveryConfigurationRepository : IReportDeliveryConfigurationService
    {
        private readonly string _connectionString;

        public ReportDeliveryConfigurationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddAsync(ReportDeliveryConfigurationModel model)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO ReportDeliveryConfigurations (DestinationType, DestinationAddress, FrequencyType, DayOfWeek, DayOfMonth, DeliveryTime, EmailAddresses, SubjectLine, BodyText, Template, FtpUrl, Password, FilePath) VALUES (@DestinationType, @DestinationAddress, @FrequencyType, @DayOfWeek, @DayOfMonth, @DeliveryTime, @EmailAddresses, @SubjectLine, @BodyText, @Template, @FtpUrl, @Password, @FilePath); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@DestinationType", model.DestinationType);
                    command.Parameters.AddWithValue("@DestinationAddress", model.DestinationAddress);
                    command.Parameters.AddWithValue("@FrequencyType", model.FrequencyType);
                    command.Parameters.AddWithValue("@DayOfWeek", model.DayOfWeek);
                    command.Parameters.AddWithValue("@DayOfMonth", model.DayOfMonth);
                    command.Parameters.AddWithValue("@DeliveryTime", model.DeliveryTime);
                    command.Parameters.AddWithValue("@EmailAddresses", string.Join(",", model.EmailAddresses));
                    command.Parameters.AddWithValue("@SubjectLine", model.SubjectLine);
                    command.Parameters.AddWithValue("@BodyText", model.BodyText);
                    command.Parameters.AddWithValue("@Template", model.Template);
                    command.Parameters.AddWithValue("@FtpUrl", model.FtpUrl);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@FilePath", model.FilePath);

                    id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            return id;
        }

        public async Task<ReportDeliveryConfigurationModel> GetByIdAsync(int id)
        {
            ReportDeliveryConfigurationModel model = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ReportDeliveryConfigurations WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            model = MapToModel(reader);
                        }
                    }
                }
            }
            return model;
        }

        public async Task<List<ReportDeliveryConfigurationModel>> GetAllAsync()
        {
            List<ReportDeliveryConfigurationModel> models = new List<ReportDeliveryConfigurationModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ReportDeliveryConfigurations", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ReportDeliveryConfigurationModel model = MapToModel(reader);
                            models.Add(model);
                        }
                    }
                }
            }
            return models;
        }

        public async Task UpdateAsync(int id, ReportDeliveryConfigurationModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE ReportDeliveryConfigurations SET DestinationType = @DestinationType, DestinationAddress = @DestinationAddress, FrequencyType = @FrequencyType, DayOfWeek = @DayOfWeek, DayOfMonth = @DayOfMonth, DeliveryTime = @DeliveryTime, EmailAddresses = @EmailAddresses, SubjectLine = @SubjectLine, BodyText = @BodyText, Template = @Template, FtpUrl = @FtpUrl, Password = @Password, FilePath = @FilePath WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@DestinationType", model.DestinationType);
                    command.Parameters.AddWithValue("@DestinationAddress", model.DestinationAddress);
                    command.Parameters.AddWithValue("@FrequencyType", model.FrequencyType);
                    command.Parameters.AddWithValue("@DayOfWeek", model.DayOfWeek);
                    command.Parameters.AddWithValue("@DayOfMonth", model.DayOfMonth);
                    command.Parameters.AddWithValue("@DeliveryTime", model.DeliveryTime);
                    command.Parameters.AddWithValue("@EmailAddresses", string.Join(",", model.EmailAddresses));
                    command.Parameters.AddWithValue("@SubjectLine", model.SubjectLine);
                    command.Parameters.AddWithValue("@BodyText", model.BodyText);
                    command.Parameters.AddWithValue("@Template", model.Template);
                    command.Parameters.AddWithValue("@FtpUrl", model.FtpUrl);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@FilePath", model.FilePath);
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DELETE FROM ReportDeliveryConfigurations WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private ReportDeliveryConfigurationModel MapToModel(SqlDataReader reader)
        {
            return new ReportDeliveryConfigurationModel
            {
                Id = (int)reader["Id"],
                DestinationType = (DestinationTypeModel)reader["DestinationType"],
                DestinationAddress = (string)reader["DestinationAddress"],
                FrequencyType = (FrequencyTypeModel)reader["FrequencyType"],
                DayOfWeek = (string)reader["DayOfWeek"],
                DayOfMonth = (string)reader["DayOfMonth"],
                DeliveryTime = (TimeSpan)reader["DeliveryTime"],
                EmailAddresses = ((string)reader["EmailAddresses"]).Split(','),
                SubjectLine = (string)reader["SubjectLine"],
                BodyText = (string)reader["BodyText"],
                Template = (string)reader["Template"],
                FtpUrl = (string)reader["FtpUrl"],
                Password = (string)reader["Password"],
                FilePath = (string)reader["FilePath"],
            };
        }
    }
}