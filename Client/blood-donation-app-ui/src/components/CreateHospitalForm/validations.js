import { object, string, number, date, InferType } from 'yup';

const requiredMessage = "Bu alan zorunludur!"
const hospitalSchema = object({
  name: string().required(requiredMessage),
  //phoneNumber: string().matches("\+90\(\d{3}\)\d{3}-\d{2}-\d{2}", "Lütfen doğru formatta telefon numarası giriniz"),
  address: string().required(requiredMessage)
});
export default hospitalSchema