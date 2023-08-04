import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import AuthLayout from './layouts/AuthLayout';
import DefaultLayout from './layouts/DefaultLayout';
import Login from './pages/Login';
import Register from './pages/Register';
import Home from './pages/Home';
import Hospitals from './pages/Hospitals';
import CreateHospital from './pages/CreateHospital';
import CreateRequest from './pages/CreateRequest';
import Requests from './pages/Requests';
import UpdateHospital from './pages/UpdateHospital';
import UpdateRequest from './pages/UpdateRequest';
import { DefaultContextProvider } from './contexts/DefaultContext';
import EmployeeManagement from './pages/EmployeeManagement';

function App() {
  return (
    <div>
       <DefaultContextProvider>
        <BrowserRouter>
          <Routes>
            <Route element={<AuthLayout />}>
              <Route index path='/login' element={<Login />}></Route>
              <Route path='/register' element={<Register />}></Route>
            </Route>
            <Route element={<DefaultLayout />}>
              <Route path='/' element={<Home />}></Route>
              <Route path='/hospitals' element={<Hospitals />}></Route>
              <Route path='/hospitals/createHospital' element={<CreateHospital />}></Route>
              <Route path='/hospitals/updateHospital/:id' element={<UpdateHospital />}></Route>
              <Route path='/hospitals/employeeManagement/:id' element={<EmployeeManagement />}></Route>

              <Route path='/requests' element={<Requests />}></Route>
              <Route path='/requests/createRequest' element={<CreateRequest />}></Route>
              <Route path='/requests/updateRequest/:id' element={<UpdateRequest />}></Route>
            </Route>
          </Routes>
        </BrowserRouter>
        </DefaultContextProvider>
    </div>
  );
}

export default App;
