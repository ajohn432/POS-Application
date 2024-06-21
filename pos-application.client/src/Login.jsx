import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./App.css";
import axios from "axios";
import "./Login.css";

function Login() {
  const [employeeId, setEmployeeId] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  const handleLogin = async e => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7007/api/auth/login",
        {
          employeeId,
          password
        }
      );

      if (response.status === 200) {
        setMessage("Login successful!");
        localStorage.setItem("token", response.data.token);
        navigate("/app");
      }
    } catch (error) {
      setMessage("Invalid. Please try again.");
    }
  };

  return (
    <div className="login-container">
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <div>
          <label>Employee ID: </label>
          <input
            type="text"
            value={employeeId}
            onChange={e => setEmployeeId(e.target.value)}
            required
          />
        </div>
        <div>
          <label className="passwordLabel">
            Password: &nbsp;&nbsp;&nbsp;&nbsp;
          </label>
          <input
            type="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="buttonContainer">
          <button className="loginBtn" type="submit">
            Login
          </button>
        </div>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}

export default Login;
