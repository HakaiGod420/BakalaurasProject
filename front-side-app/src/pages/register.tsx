import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import RegisterForm from "../components/RegisterForm";
import { CheckJWTAndSession } from "../services/midlewear/AuthVerify";

function Register() {
  const [, setTokenValidation] = useState<boolean | undefined>(false);
  const navigate = useNavigate();

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      setTokenValidation(check);

      if (check === true) {
        navigate("/");
      }
    };
    validateToken();
  }, [navigate]);

  return <RegisterForm />;
}

export default Register;
