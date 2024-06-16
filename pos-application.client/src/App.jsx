import { useState } from 'react';
import './App.css';
import axios from 'axios';

function App() {
    const [employeeId, setEmployeeId] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7007/api/auth/login', {
                employeeId,
                password
            });

            if (response.status === 200) {
                setMessage('Login successful!');
                // Handle successful login, e.g., store token, redirect, etc.
                localStorage.setItem('token', response.data.token);
            }
        } catch (error) {
            setMessage('Invalid credentials. Please try again.');
        }
    };

    return (
        <div className="login-container">
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <div>
                    <label>Employee ID:</label>
                    <input
                        type="text"
                        value={employeeId}
                        onChange={(e) => setEmployeeId(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Login</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
}

export default App;