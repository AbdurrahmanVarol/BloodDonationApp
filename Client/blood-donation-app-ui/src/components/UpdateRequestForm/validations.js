import { object, string, number, date, InferType } from 'yup';

const requiredMessage = "Bu alan zorunludur!"
const requestSchema = object({
  quantity: string().required(requiredMessage)
});
export default requestSchema