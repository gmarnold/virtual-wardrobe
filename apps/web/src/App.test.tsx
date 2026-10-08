import { fireEvent, render, screen } from '@testing-library/react'
import { afterEach, describe, expect, it, vi } from 'vitest'
import App from './App'

const garment = {
  id: '10000000-0000-4000-8000-000000000001', name: 'Lavender Cardigan',
  category: 'Top', subtype: 'Cardigan', colors: ['Lavender'],
  seasons: ['Spring', 'Fall'], styleTags: ['Romantic', 'Cozy'], occasions: ['Casual'],
  status: 'Active', brand: null, size: null, notes: null,
  createdAt: '2026-01-01T00:00:00Z', updatedAt: '2026-01-01T00:00:00Z',
}

afterEach(() => vi.unstubAllGlobals())

describe('closet', () => {
  it('shows loading while the request is pending', () => {
    vi.stubGlobal('fetch', vi.fn(() => new Promise(() => {})))
    render(<App />)
    expect(screen.getByRole('status')).toHaveTextContent('Opening your closet')
  })

  it('renders garments and relationship tags from the API', async () => {
    const fetch = vi.fn().mockResolvedValue(new Response(JSON.stringify([garment])))
    vi.stubGlobal('fetch', fetch)
    render(<App />)
    expect(await screen.findByRole('heading', { name: 'Lavender Cardigan' })).toBeVisible()
    expect(screen.getByText('Lavender', { selector: 'p' })).toBeVisible()
    expect(screen.getByText('Spring')).toBeVisible()
    expect(screen.getByText('Romantic')).toBeVisible()
    expect(fetch.mock.calls[0][0]).toMatch(/\/api\/garments$/)
  })

  it('shows a useful network error and allows a successful retry', async () => {
    vi.stubGlobal('fetch', vi.fn()
      .mockRejectedValueOnce(new TypeError('Network unavailable'))
      .mockResolvedValueOnce(new Response(JSON.stringify([garment]))))
    render(<App />)
    expect(await screen.findByRole('alert')).toHaveTextContent('Check that the API and database are running')
    fireEvent.click(screen.getByRole('button', { name: 'Try again' }))
    expect(await screen.findByRole('heading', { name: 'Lavender Cardigan' })).toBeVisible()
    expect(screen.queryByRole('alert')).not.toBeInTheDocument()
  })

  it('shows the error state for an unsuccessful HTTP response', async () => {
    vi.stubGlobal('fetch', vi.fn().mockResolvedValue(new Response('', { status: 503 })))
    render(<App />)
    expect(await screen.findByRole('alert')).toHaveTextContent('Your closet couldn’t load')
  })

  it('shows an empty state when the API returns no garments', async () => {
    vi.stubGlobal('fetch', vi.fn().mockResolvedValue(new Response('[]')))
    render(<App />)
    expect(await screen.findByRole('heading', { name: 'Your closet is empty' })).toBeVisible()
    expect(screen.getByText('0 pieces')).toBeVisible()
  })
})
