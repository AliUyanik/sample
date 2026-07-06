import { useEffect, useState } from 'react';
import './App.css';

type ConfigResponse = {
  displayMessage: string;
};

type GreetingResponse = {
  message: string;
};

function App() {
  const [message, setMessage] = useState<string>('Yükleniyor…');
  const [greeting, setGreeting] = useState<string>('Yükleniyor…');
  const [configError, setConfigError] = useState<string | null>(null);
  const [greetingError, setGreetingError] = useState<string | null>(null);

  useEffect(() => {
    void fetch('/api/config')
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return res.json() as Promise<ConfigResponse>;
      })
      .then((data) => setMessage(data.displayMessage))
      .catch((err: unknown) => {
        setConfigError(err instanceof Error ? err.message : 'Bilinmeyen hata');
      });

    void fetch('/api/greeting')
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return res.json() as Promise<GreetingResponse>;
      })
      .then((data) => setGreeting(data.message))
      .catch((err: unknown) => {
        setGreetingError(err instanceof Error ? err.message : 'Bilinmeyen hata');
      });
  }, []);

  return (
    <main className="page">
      <h1>Test Project</h1>
      <p className="lead">Vault / ortam yapılandırması:</p>
      {configError ? (
        <p className="error" role="alert">
          API hatası: {configError}
        </p>
      ) : (
        <p className="message">{message}</p>
      )}

      <p className="lead lead-spaced">API karşılama metni:</p>
      {greetingError ? (
        <p className="error" role="alert">
          API hatası: {greetingError}
        </p>
      ) : (
        <p className="message message-secondary">{greeting}</p>
      )}
    </main>
  );
}

export default App;
