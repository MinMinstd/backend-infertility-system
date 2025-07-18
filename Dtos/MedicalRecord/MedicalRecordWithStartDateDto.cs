namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordWithStartDateDto
    {
        public int MedicalRecordId { get; set; }

        public DateOnly StartDate { get; set; }
    }
}
