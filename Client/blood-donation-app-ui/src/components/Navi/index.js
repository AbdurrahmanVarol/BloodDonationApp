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
  const { token, userName, userRole, clearData } = useContext(DefaultContext);

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
            {
              
              token && userRole != 3 ? (
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret>
                    Hastane İşlemleri
                  </DropdownToggle>
                  <DropdownMenu end>
                  {
                    userRole == 1 ? (
                      <><DropdownItem>
                          <NavLink className="nav-link" to="/hospitals" state={""}>
                            Hastaneler
                          </NavLink>
                        </DropdownItem><DropdownItem>
                            <NavLink className="nav-link" to="/hospitals/createHospital" state={""}>
                              Hastane Oluştur
                            </NavLink>
                          </DropdownItem></>
                    ):<></>
                  }
                    <DropdownItem>
                      <NavLink className="nav-link" to="/requests" state={""}>
                        Talepler
                      </NavLink>
                    </DropdownItem>
                    <DropdownItem>
                      <NavLink className="nav-link" to="/requests/createRequest" state={""}>
                        Talep Oluştur
                      </NavLink>
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>
              ) : <></>
            }

          </Nav>
          <Button
            color="danger"
            onClick={() => {
              clearData();
              navigate("/login");
            }}
          >
            {
              userName ? `${userName.split(" ")[0]}(Çıkış)` : 'Çıkış Yap'
            }
          </Button>
        </Collapse>
      </Navbar>
    </div>
  );
}

export default Navi