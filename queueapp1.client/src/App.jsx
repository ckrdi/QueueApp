import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [data, setData] = useState();
    const [value, setValue] = useState("");

    useEffect(() => {
        populateData();
    }, []);

    const contents = data === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Value</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                {data.map(item =>
                    <tr key={item.id}>
                        <td>{item.createdDateTime}</td>
                        <td>{item.value}</td>
                        <td>{item.status}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Queue management</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
            <div style={{
                display: 'flex',
                marginBottom: '1em'
            }}>
                <button onClick={() => populateData()}>Update list</button>
            </div>
            <div>
                <h3>Send queue</h3>
                <input type="text" value={value} onChange={(e) => setValue(e.target.value)} />
                <button onClick={() => sendData()}>Send</button>
            </div>
        </div>
    );
    
    async function populateData() {
        const response = await fetch('https://localhost:7292/Queue');
        const data = await response.json();

        setData(data);
    }

    async function sendData() {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request('https://localhost:7292/Queue', {
            method: "POST",
            body: JSON.stringify({ value: value }),
            headers: headers,
        });

        setValue("");

        const response = await fetch(request);
        const result = await response.json();
        console.log(result);
    }
}

export default App;