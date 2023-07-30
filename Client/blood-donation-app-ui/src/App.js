import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import { DefaultContextProvider } from './contexts/DefaultContext';
import AuthLayout from './layouts/AuthLayout';
import DefaultLayout from './layouts/DefaultLayout';
import Login from './pages/Login';
import { useEffect } from 'react';
import Register from './pages/Register';
import Home from './pages/Home';
import Hospitals from './pages/Hospitals';
import CreateHospital from './pages/CreateHospital';
import CreateRequest from './pages/CreateRequest';
import Requests from './pages/Requests';

function App() { 
  return (
    <div>
      <DefaultContextProvider>
        <BrowserRouter>
          <Routes>
            <Route element={<AuthLayout/>}>
            <Route index path='/login' element={<Login/>}></Route>
            <Route path='/register' element={<Register/>}></Route>
            </Route>
            <Route element={<DefaultLayout/>}>
            <Route path='/' element={<Home/>}></Route>
            <Route path='/hospitals' element={<Hospitals/>}></Route>
            <Route path='/hospitals/createHospital' element={<CreateHospital/>}></Route>
            <Route path='/hospitals/updateHospital/:hospitalId' element={<CreateHospital/>}></Route>
            <Route path='/hospitals/employeeManagement' element={<CreateHospital/>}></Route>
            <Route path='/createHospital' element={<CreateHospital/>}></Route>

            <Route path='/requests' element={<Requests/>}></Route>
            <Route path='/createRequest' element={<CreateRequest/>}></Route>
            <Route path='/' element={<Home/>}></Route>
            </Route>
          </Routes>
        </BrowserRouter>
      </DefaultContextProvider>


    </div>
  );
}

export default App;
