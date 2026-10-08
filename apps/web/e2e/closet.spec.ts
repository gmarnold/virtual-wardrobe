import { expect, test } from '@playwright/test'

test('shows the PostgreSQL-backed demo wardrobe', async ({ page }) => {
  await page.goto('/')
  await expect(page.getByRole('heading', { name: 'My Closet' })).toBeVisible()
  await expect(page.getByRole('heading', { name: 'Lavender Cardigan' })).toBeVisible()
  await expect(page.getByRole('list', { name: 'Garments' }).getByRole('listitem')).toHaveCount(10)
})

test('shows a useful error when the API cannot be reached', async ({ page }) => {
  await page.route('**/api/garments', route => route.abort('connectionrefused'))
  await page.goto('/')
  await expect(page.getByRole('alert')).toContainText('Check that the API and database are running')
  await expect(page.getByRole('button', { name: 'Try again' })).toBeVisible()
})
