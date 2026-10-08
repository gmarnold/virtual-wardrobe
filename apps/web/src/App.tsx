import { useEffect, useState } from 'react'
import { getGarments, type Garment } from './api'
import { productName } from './config'
import './App.css'

type ClosetState =
  | { kind: 'loading' }
  | { kind: 'ready'; garments: Garment[] }
  | { kind: 'error' }

export default function App() {
  const [state, setState] = useState<ClosetState>({ kind: 'loading' })
  const [attempt, setAttempt] = useState(0)

  useEffect(() => {
    const controller = new AbortController()
    getGarments(controller.signal)
      .then(garments => setState({ kind: 'ready', garments }))
      .catch(() => { if (!controller.signal.aborted) setState({ kind: 'error' }) })
    return () => controller.abort()
  }, [attempt])

  const retry = () => { setState({ kind: 'loading' }); setAttempt(value => value + 1) }

  return (
    <div className="app-shell">
      <header className="site-header">
        <a className="brand" href="/" aria-label={`${productName} home`}>
          <span className="brand-mark" aria-hidden="true">W</span>{productName}
        </a>
        <span className="demo-label">Demo closet</span>
      </header>
      <main>
        <div className="closet-heading">
          <p className="eyebrow">A little room for possibility</p>
          <h1>My Closet</h1>
          <p className="intro">Every piece has a place. Start with what’s here, and imagine what goes together.</p>
        </div>
        <div className="collection-bar">
          <h2>The collection</h2>
          <span>{state.kind === 'ready' ? `${state.garments.length} ${state.garments.length === 1 ? 'piece' : 'pieces'}` : 'Wardrobe'}</span>
        </div>
        {state.kind === 'loading' && <div className="state-panel" role="status">Opening your closet…</div>}
        {state.kind === 'error' && (
          <div className="state-panel" role="alert">
            <h2>Your closet couldn’t load</h2>
            <p>Check that the API and database are running, then try again.</p>
            <button onClick={retry}>Try again</button>
          </div>
        )}
        {state.kind === 'ready' && (state.garments.length === 0
          ? <div className="state-panel"><h2>Your closet is empty</h2><p>Garments will appear here when they’re added to your wardrobe.</p></div>
          : <ul className="garment-grid" aria-label="Garments">
              {state.garments.map(garment => (
                <li key={garment.id}>
                  <article className="garment-card">
                    <div className="card-swatch" data-category={garment.category}>
                      <span className="swatch-type">{garment.subtype}</span>
                      <span className="swatch-monogram" aria-hidden="true">{garment.name.charAt(0)}</span>
                      <span className="swatch-note">Demo piece · No photograph</span>
                    </div>
                    <div className="card-content">
                      <p className="category">{garment.category} / {garment.subtype}</p>
                      <h3>{garment.name}</h3>
                      <p className="colors"><span>Color</span> {garment.colors.join(' · ')}</p>
                      <div className="tags" aria-label="Seasons and styles">
                        {garment.seasons.map(tag => <span className="tag season" key={`season-${tag}`}>{tag}</span>)}
                        {garment.styleTags.map(tag => <span className="tag" key={`style-${tag}`}>{tag}</span>)}
                      </div>
                    </div>
                  </article>
                </li>
              ))}
            </ul>)}
      </main>
      <footer>Fictional pieces, real possibilities. This collection uses synthetic demo data.</footer>
    </div>
  )
}
