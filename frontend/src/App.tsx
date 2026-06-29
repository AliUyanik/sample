import { useEffect, useState } from 'react';
import './App.css';

type ConfigResponse = {
  displayMessage: string;
};

function App() {
  const [message, setMessage] = useState<string>('Yükleniyor…');
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    void fetch('/api/config')
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return res.json() as Promise<ConfigResponse>;
      })
      .then((data) => setMessage(data.displayMessage))
      .catch((err: unknown) => {
        setError(err instanceof Error ? err.message : 'Bilinmeyen hata');
      });
  }, []);

  return (
    <main className="page">
      <h1>Test Project</h1>
      <p className="lead">Ana sayfa — backend appsettings değeri:</p>
      {error ? (
        <p className="error" role="alert">
          API hatası: {error}
        </p>
      ) : (
        <p className="message">{message}</p>
      )}
    </main>
  );
}

export default App;
