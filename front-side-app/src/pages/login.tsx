import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import LoginComponent from "../components/LoginComponent";
import { CheckJWTAndSession } from "../services/midlewear/AuthVerify";

function Login() {
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

  return (
    <div className="bg-white text-black">
      <LoginComponent />
    </div>
  );
}

export default Login;
