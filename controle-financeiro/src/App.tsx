import styles from './App.module.css';
import { PessoasPage } from './pages/PessoasPage';
import { RelatoriosPage } from './pages/RelatoriosPage';
import { Sidebar } from './components/common/Slidebar/Sidebar';
import { useState } from 'react';
import { CategoriasPage } from './pages/CategoriasPage';
import { TransacoesPage } from './pages/TransacoesPage';

const menuItems = [
    { id: 'pessoas', label: 'Pessoas', icon: '👥', path: '/pessoas' },
    { id: 'categorias', label: 'Categorias', icon: '📂', path: '/categorias' },
    { id: 'transacoes', label: 'Transações', icon: '💰', path: '/transacoes' },
    { id: 'relatorios', label: 'Relatórios', icon: '📊', path: '/relatorios' },
];

function App() {
    const [currentPath, setCurrentPath] = useState('/pessoas');

    const renderPage = () => {
        switch (currentPath) {
            case '/pessoas':
                return <PessoasPage />;
            case '/categorias':
                return <CategoriasPage />;
            case '/transacoes':
                return <TransacoesPage />;
            case '/relatorios':
                return <RelatoriosPage />;
            default:
                return <PessoasPage />;
        }
    };

    return (
        <div className={styles.app}>
            <Sidebar menuItems={menuItems} activePath={currentPath} onNavigate={setCurrentPath} />
            <main className={styles.main}>
                <div className={styles.content}>
                    {renderPage()}
                </div>
            </main>
        </div>
    );
}

export default App;