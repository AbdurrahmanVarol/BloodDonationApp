import React, { useContext, useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import {
  Button,
  Collapse,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  Nav,
  NavItem,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  UncontrolledDropdown,
} from "reactstrap";
import DefaultContext from "../../contexts/DefaultContext";

const Navi = () => {
    const [isOpen, setIsOpen] = useState(false);
    const navigate = useNavigate();
    const { clearData } = useContext(DefaultContext);
  
    const toggle = () => setIsOpen(!isOpen);
    return (
      <div>
        <Navbar className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow">
          <NavbarBrand className="brand" href="/">
            Kan Bağışı
          </NavbarBrand>
          <NavbarToggler onClick={toggle} />
          <Collapse isOpen={isOpen} navbar>
            <Nav className="me-auto" navbar>
              <NavItem>
                <NavLink className="nav-link" to="/">
                  Anasayfa
                </NavLink>
              </NavItem>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                Hastane İşlemleri
                </DropdownToggle>
                <DropdownMenu end>
                  <DropdownItem>
                    <NavLink className="nav-link" to="/hospitals" state={""}>
                    Hastaneler
                    </NavLink>
                  </DropdownItem>
                  <DropdownItem>
                    <NavLink className="nav-link" to="/hospitals/createHospital" state={""}>
                    Hastane Oluştur
                    </NavLink>
                  </DropdownItem>
                  <DropdownItem>
                    <NavLink className="nav-link" to="/requests" state={""}>
                     Talepler
                    </NavLink>
                  </DropdownItem>
                  <DropdownItem>
                    <NavLink className="nav-link" to="/createRequest" state={""}>
                      Talep Oluştur
                    </NavLink>
                  </DropdownItem>
                </DropdownMenu>
              </UncontrolledDropdown>              
            </Nav>
            <Button
              color="danger"
              onClick={() => {
                clearData();
                navigate("/login");
              }}
            >
              Çıkış Yap
            </Button>
          </Collapse>
        </Navbar>
      </div>
    );
}

export default Navi